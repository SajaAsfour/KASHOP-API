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
        private readonly IProductRepository _productRepository;

        public CartSerivce(ICartRepository cartRepository , IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }
        public async Task <bool> AddToCartAsync (AddToCartRequest request, string UserId)
        {
            var product = await _productRepository.GetOneAsync(p => p.Id == request.ProductId);
            if (product is null) return false;

            var ExsistingItem = await _cartRepository.GetOneAsync(
                c=>c.ProductId == request.ProductId && c.UserId == UserId
                );

            var currentCount = ExsistingItem?.Count ?? 0;
            var newCount = currentCount + request.Count;

            if (newCount > product.Quantity) return false;
            
            if(ExsistingItem != null )
            {
                ExsistingItem.Count += newCount;
                await _cartRepository.UpdateAsync( ExsistingItem );
            }
            else
            {
                var cartItems = request.Adapt<Cart>();
                cartItems.UserId = UserId;
                await _cartRepository.CreateAsync( cartItems );
            }

            return true;
        }
    }
}
