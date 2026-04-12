using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public interface ICartSerivce
    {
        Task<bool> AddToCartAsync (AddToCartRequest request, string UserId);
        Task<List<CartResponse>> GetCartAsync(string userId);
        Task<bool> UpdateQuantityAsync(int productId, int count, string userId);
        Task<bool> RemoveItemAsync(int productId , string userId);
        Task<bool> ClearCartAsync(string userId);

    }
}
