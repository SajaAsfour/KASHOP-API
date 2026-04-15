using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositry;
using Mapster;
using MapsterMapper;
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
        private readonly IMapper _mapper;

        public CartSerivce(ICartRepository cartRepository 
            , IProductRepository productRepository
            , IMapper mapper)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _mapper = mapper;
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
                var cartItems = _mapper.Map<Cart>(request);
                cartItems.UserId = UserId;
                await _cartRepository.CreateAsync( cartItems );
            }

            return true;
        }

        public Task<bool> ClearCartAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CartResponse>> GetCartAsync(string userId)
        {
            var items = await _cartRepository.GetAllAsync(
                filter : x => x.UserId == userId,
                includes : new string[]
                {
                    nameof(Cart.Product),
                    $"{nameof(Cart.Product)}.{nameof(Product.Translations)}"
                }
                );
            return _mapper.Map<List<CartResponse>>(items);
        }

        public async Task<bool> RemoveItemAsync(int productId, string userId)
        {
            var item = await _cartRepository.GetOneAsync(
                c => c.ProductId == productId && c.UserId == userId
                );

            if(item is null) return false;

            return await _cartRepository.DeleteAsync( item );
        }

        public Task<bool> UpdateQuantityAsync(int productId, int count, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
