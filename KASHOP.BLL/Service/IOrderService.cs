using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetUserOrdersAsync(string userId);
        Task<OrderDetailsResponse?> GetUserOrderAsync(string userId, int orderId);
        Task<bool> CancleOrderAsync(string userId , int orderId);
        Task<List<OrderResponse>> GetAllOrdersAsync(OrderStatusEnum status);
    }
}
