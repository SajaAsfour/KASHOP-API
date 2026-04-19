using KASHOP.BLL.Service;
using KASHOP.DAL.Repositry;
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

            Services.AddScoped<IFileService, BLL.Service.FileService>();
            Services.AddScoped<IProductRepository, ProductRepository>();
            Services.AddScoped<IProductService, BLL.Service.ProductService>();

            Services.AddScoped<IUrlService, UrlService>();

            Services.AddScoped<IBrandService, BrandService>();
            Services.AddScoped<IBrandRepository, BrandRepository>();

            Services.AddScoped<ICartRepository, CartRepository>();
            Services.AddScoped<ICartSerivce, CartSerivce>();

            Services.AddScoped<ICheckoutService, BLL.Service.CheckoutService>();

            Services.AddScoped<IOrderRepository, OrderRepository>();

            Services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = Configuration["Stripe:SecretKey"];

            return Services;
        }
    }
}
