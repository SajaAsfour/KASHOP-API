using KASHOP.BLL.Service.Authentication;
using KASHOP.BLL.Service.Brands;
using KASHOP.BLL.Service.Carts;
using KASHOP.BLL.Service.Categories;
using KASHOP.BLL.Service.Checkouts;
using KASHOP.BLL.Service.Email;
using KASHOP.BLL.Service.Files;
using KASHOP.BLL.Service.Orders;
using KASHOP.BLL.Service.Products;
using KASHOP.BLL.Service.Reviews;
using KASHOP.BLL.Service.Urls;
using KASHOP.BLL.Service.UserManagements;
using KASHOP.DAL.Repository.Brands;
using KASHOP.DAL.Repository.Carts;
using KASHOP.DAL.Repository.Categories;
using KASHOP.DAL.Repository.Orders;
using KASHOP.DAL.Repository.Products;
using KASHOP.DAL.Repository.Reviews;
using KASHOP.DAL.utils;
using Stripe;

namespace KASHOP.PL.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services,
            IConfiguration Configuration)
        {
            Services.AddScoped<ICategoryRepository, CategoryRepository>();

            Services.AddScoped<ICategoryService, CategoryService>();

            Services.AddScoped<IAuthenticationService, AuthenticationService>();

            Services.AddScoped<ISeedData, RoleSeedData>();

            Services.AddTransient<IEmailSender, EmailSender>();

            Services.AddScoped<IFileService, BLL.Service.Files.FileService>();
            Services.AddScoped<IProductRepository, ProductRepository>();
            Services.AddScoped<IProductService, BLL.Service.Products.ProductService>();

            Services.AddScoped<IUrlService, UrlService>();

            Services.AddScoped<IBrandService, BrandService>();
            Services.AddScoped<IBrandRepository, BrandRepository>();

            Services.AddScoped<ICartRepository, CartRepository>();
            Services.AddScoped<ICartSerivce, CartSerivce>();

            Services.AddScoped<ICheckoutService, BLL.Service.Checkouts.CheckoutService>();

            Services.AddScoped<IOrderRepository, OrderRepository>();

            Services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = Configuration["Stripe:SecretKey"];

            Services.AddScoped<IOrderService, OrderService>();

            Services.AddScoped<IUserManagementSerivce, UserManagementSerivce>();
            
            Services.AddScoped<IReviewRepository, ReviewRepository>();
            Services.AddScoped<IReviewService, BLL.Service.Reviews.ReviewService>();

            return Services;
        }
    }
}
