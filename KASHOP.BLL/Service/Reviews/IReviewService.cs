using KASHOP.DAL.DTO.Request;

namespace KASHOP.BLL.Service.Reviews
{
    public interface IReviewService
    {
        Task<bool> AddReviewAsync (string userId, AddReviewRequest request);
    }
}
