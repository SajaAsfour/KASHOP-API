using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;

namespace KASHOP.BLL.Service.Orders
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetUserOrdersAsync(string userId);
        Task<OrderDetailsResponse?> GetUserOrderAsync(string userId, int orderId);
        Task<bool> CancleOrderAsync(string userId , int orderId);
        Task<List<OrderResponse>> GetAllOrdersAsync(OrderStatusEnum status);
        Task<bool> ChangeOrderStatusAsync(int orderId, ChangeOrderStatusRequest request);
    }
}
