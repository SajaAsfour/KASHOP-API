using KASHOP.BLL.Service.Carts;
using KASHOP.BLL.Service.Email;
using KASHOP.DAL.DTO.Request.Checkouts;
using KASHOP.DAL.DTO.Response.Checkouts;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository.Carts;
using KASHOP.DAL.Repository.Orders;
using KASHOP.DAL.Repository.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Stripe.Checkout;

namespace KASHOP.BLL.Service.Checkouts
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderRepository _orderRepository;
        private readonly ICartSerivce _cartSerivce;
        private readonly IProductRepository _productRepository;
        private readonly IEmailSender _emailSender;

        public CheckoutService(ICartRepository cartRepository, UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor , IOrderRepository orderRepository
            ,ICartSerivce cartSerivce ,IProductRepository productRepository
            ,IEmailSender emailSender)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _orderRepository = orderRepository;
            _cartSerivce = cartSerivce;
            _productRepository = productRepository;
            _emailSender = emailSender;
        }

        public async Task<CheckoutResponse> HandleSuccess(string sessionId)
        {
            var order = await _orderRepository.GetOneAsync(
                o => o.StripeSessionId  == sessionId
                ,includes: new[]
                {
                    nameof(Order.OrderItems),
                    $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}",
                    $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}.{nameof(Product.Translations)}"
                } );

            order.OrderStatus = OrderStatusEnum.Paid;
            await _orderRepository.UpdateAsync(order);

            await _cartSerivce.ClearCartAsync(order.UserId);

            var user = await _userManager.FindByIdAsync(order.UserId);

            await _emailSender.SendEmailAsync(user.Email, "order confirmed",
                "<h2>your order has beed placed successfully</h2>");

            var lowStockProducts = await _productRepository.DecreaseQuantityAsync(order.OrderItems);

            foreach(var item in lowStockProducts)
            {
                if(lowStockProducts != null)
                {
                    await _emailSender.SendEmailAsync("sajanazih2004@gmail.com", "low stock alert"
                        , $"<h2>product {item.Translations.FirstOrDefault( t => t.Language == "en").Name
                        } current quantity : {item.Quantity}</h2>");
                }
            }

            return new CheckoutResponse()
            {
                Success = true,
                OrderId = order.Id
            };
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

            var order = new Order() 
            {
                UserId = userId,
                City = city,
                Street = street,
                PhoneNumber = phoneNumber,
                PaymentMethod = request.PaymentMethod,
                AmountPaid = cartItems.Sum(c => c.Product.Price * c.Count),
                OrderStatus = OrderStatusEnum.Pending,
                OrderItems = cartItems.Select(c => new OrderItem
                {
                    ProductId = c.ProductId,
                    Quantity = c.Count,
                    UnitPrice = c.Product.Price,
                    TotalPrice = c.Product.Price * c.Count
                }).ToList()
            };

            await _orderRepository.CreateAsync(order);

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

                    SuccessUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}:" +
                    $"//{_httpContextAccessor.HttpContext.Request.Host}/api/checkouts/success" +
                    $"?sessionId={{CHECKOUT_SESSION_ID}}",
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

                order.StripeSessionId = session.Id;
                await _orderRepository.UpdateAsync(order);

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
