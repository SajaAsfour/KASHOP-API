using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckoutService(ICartRepository cartRepository, UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<CheckoutResponse> ProcessCheckout(string userId, CheckoutRequest request)
        {
            var cartItems = await _cartRepository.GetAllAsync(
                filter: c => c.UserId == userId,
                includes : new string[]
                {
                    nameof(Cart.Product),
                    $"{nameof(Cart.Product)}.{nameof(Product.Translations)}"
                }
                );

            if (!cartItems.Any())
                return new CheckoutResponse
                {
                    Success = false,
                    Error = "carts is empty"
                };

            var user = await _userManager.FindByIdAsync(userId);

            var city = request.City ?? user.City;
            if (city is null)
                return new CheckoutResponse
                {
                    Success = false,
                    Error = "city is requierd"
                };

            var street = request.Street ?? user.Street;
            if (street is null)
                return new CheckoutResponse
                {
                    Success = false,
                    Error = "street is requierd"
                };

            var phoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            if (phoneNumber is null)
                return new CheckoutResponse
                {
                    Success = false,
                    Error = "phoneNumber is requierd"
                };

            foreach (var item in cartItems)
            {
                if (item.Count > item.Product.Quantity)
                    return new CheckoutResponse
                    {
                        Success = false,
                        Error = "dosn't have enough stock"
                    };
            }

            if (request.PaymentMethod == PaymentMethodEnum.Cash)
            {
                return new CheckoutResponse
                {
                    Success = true
                };
            }

            if (request.PaymentMethod == PaymentMethodEnum.Visa)
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    Mode = "payment",

                    SuccessUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/checkout/success",
                    CancelUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/checkout/cancel",

                    LineItems = new List<SessionLineItemOptions>()
                };

                foreach(var item in cartItems)
                {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Translations.FirstOrDefault(
                                    t => t.Language == "en").Name,
                            },
                            UnitAmount = (long) (item.Product.Price * 100),
                        },
                        Quantity = item.Count,
                    }
                    );
                }
                var service = new SessionService();
                var session = service.Create(options);

                return new CheckoutResponse
                {
                    Success = true,
                    StripeUrl = session.Url
                };
            }

            return new CheckoutResponse
            {
                Success = false,
                Error = "Invalid Payment Method"
            };
        }
    }
}
