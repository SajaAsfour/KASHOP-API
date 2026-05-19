using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositry;
using MapsterMapper;

namespace KASHOP.BLL.Service.Reviews
{
    public class ReviewService : IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IOrderRepository orderRepository, IMapper mapper, IReviewRepository reviewRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }
        public async Task<bool> AddReviewAsync(string userId, AddReviewRequest request)
        {
            var purchedOrder = await _orderRepository.GetOneAsync(
                filter: o => o.UserId == userId && o.OrderStatus == OrderStatusEnum.Delivered
                && o.OrderItems.Any(oi => oi.ProductId == request.ProductId),
                includes: new[]
                {
                    nameof(Order.OrderItems)
                }
                );

            if (purchedOrder == null) return false;

            var alreadyReviews = await _reviewRepository.GetOneAsync(
                filter: r => r.UserId == userId && r.ProductId == request.ProductId
                );

            if (alreadyReviews != null) return false;

            var review = _mapper.Map<Review>(request);
            review.UserId = userId;

            await _reviewRepository.CreateAsync(review);

            return true;
        }
    }
}
