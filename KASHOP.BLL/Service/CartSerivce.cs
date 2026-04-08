using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositry;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public class CartSerivce : ICartSerivce
    {
        private readonly ICartRepository _cartRepository;

        public CartSerivce(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task AddToCartAsync (AddToCartRequest request, string UserId)
        {
            var ExsistingItem = await _cartRepository.GetOneAsync(
                c=>c.ProductId == request.ProductId && c.UserId == UserId
                );

            if(ExsistingItem != null )
            {
                ExsistingItem.Count += request.Count;
                await _cartRepository.UpdateAsync( ExsistingItem );
            }
            else
            {
                var cartItems = request.Adapt<Cart>();
                cartItems.UserId = UserId;
                await _cartRepository.CreateAsync( cartItems );
            }
        }
    }
}
