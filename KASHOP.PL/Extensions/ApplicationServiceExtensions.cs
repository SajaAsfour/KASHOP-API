using KASHOP.BLL.Service;
using KASHOP.DAL.Repositry;
using KASHOP.DAL.utils;

namespace KASHOP.PL.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped<ICategoryRepository, CategoryRepository>();

            Services.AddScoped<ICategoryService, CategoryService>();

            Services.AddScoped<IAuthenticationService, AuthenticationService>();

            Services.AddScoped<ISeedData, RoleSeedData>();

            Services.AddTransient<IEmailSender, EmailSender>();

            Services.AddScoped<IFileService, FileService>();
            Services.AddScoped<IProductRepository, ProductRepository>();
            Services.AddScoped<IProductService, ProductService>();

            Services.AddScoped<IUrlService, UrlService>();

            Services.AddScoped<IBrandService, BrandService>();
            Services.AddScoped<IBrandRepository, BrandRepository>();

            return Services;
        }
    }
}
