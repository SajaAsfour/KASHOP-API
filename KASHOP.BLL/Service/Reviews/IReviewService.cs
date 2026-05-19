using KASHOP.DAL.DTO.Request.Reviews;

namespace KASHOP.BLL.Service.Reviews
{
    public interface IReviewService
    {
        Task<bool> AddReviewAsync (string userId, AddReviewRequest request);
    }
}
