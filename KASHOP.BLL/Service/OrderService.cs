using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositry;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository,IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<bool> CancleOrderAsync(string userId, int orderId)
        {
            var order = await _orderRepository.GetOneAsync(filter: o => o.UserId  == userId && o.Id == orderId);

            if(order == null) return false;

            if(order.OrderStatus != OrderStatusEnum.Pending) return false;

            order.OrderStatus = OrderStatusEnum.Cancelled;

            return await _orderRepository.UpdateAsync(order);
        }

        public async Task<bool> ChangeOrderStatusAsync(int orderId, ChangeOrderStatusRequest request)
        {
            var order = await _orderRepository.GetOneAsync(o => o.Id == orderId);

            if(order.OrderStatus == OrderStatusEnum.Cancelled || order.OrderStatus == OrderStatusEnum.Delivered)
                return false;

            if((int)request.Status != (int)order.OrderStatus + 1) 
                return false;

            order.OrderStatus = request.Status;
            
            return await _orderRepository.UpdateAsync(order);
        }

        public async Task<List<OrderResponse>> GetAllOrdersAsync(OrderStatusEnum status)
        {
            var orders = await _orderRepository.GetAllAsync(filter: o => o.OrderStatus == status);

            return _mapper.Map<List<OrderResponse>>(orders);
        }

        public async Task<OrderDetailsResponse?> GetUserOrderAsync(string userId, int orderId)
        {
            var order = await _orderRepository.GetOneAsync(
                filter: o => o.UserId == userId && o.Id == orderId,
                includes: new[]
                {
                    nameof(Order.OrderItems),
                    $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}",
                    $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}.{nameof(Product.Translations)}"
                }
                );
            return _mapper.Map<OrderDetailsResponse>(order);
        }

        public async Task<List<OrderResponse>> GetUserOrdersAsync(string userId)
        {
            var orders = await _orderRepository.GetAllAsync(
                filter: o => o.UserId == userId,
                includes: new[]
                {
                    nameof(Order.OrderItems),
                    $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}",
                    $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}.{nameof(Product.Translations)}"
                } 
                );

            return _mapper.Map<List<OrderResponse>>(orders);
        }
    }
}
