using KASHOP.BLL.Service.Urls;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using Mapster;
using System.Globalization;

namespace KASHOP.BLL.Mapping
{
    public static class MapsterConfig
    {
        public static void MapsterConfigRegister()
        {
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
                .Map(dest => dest.category_Id, source => source.Id)
                .Map(dest => dest.UserCreated , source => source.CreatedBy.UserName)
                .Map(dest => dest.Name , source => source.Translations.Where(
                    t => t.Language == CultureInfo.CurrentCulture.Name).Select(
                    t => t.Name).FirstOrDefault());

            TypeAdapterConfig<Product, ProductResponse>.NewConfig()
                .Map(dest => dest.UserCreated, source => source.CreatedBy.UserName)
                .Map(dest => dest.Name, source => source.Translations.Where(
                    t => t.Language == CultureInfo.CurrentCulture.Name).Select(
                    t => t.Name).FirstOrDefault())
                .Map(dest =>dest.MainImage , 
                source => MapContext.Current.GetService<IUrlService>()
                .GetImageUrl(source.MainImage))
                .Map(dest => dest.SubImages, source => source.SubImages
                       .Select(img => MapContext.Current.GetService<IUrlService>()
                       .GetImageUrl(img.ImagePath))
                       .ToList());

            TypeAdapterConfig<Brand, BrandResponse>.NewConfig()
                .Map(dest => dest.UserCreated, source => source.CreatedBy.UserName)
                .Map(dest => dest.Name, source => source.BrandTranslations.Where(
                    t => t.Language == CultureInfo.CurrentCulture.Name).Select(
                    t => t.Name).FirstOrDefault())
                .Map(dest => dest.Logo,
                source => MapContext.Current.GetService<IUrlService>()
                .GetImageUrl(source.Logo));

            TypeAdapterConfig<ProductUpdateRequest, Product>.NewConfig()
                .IgnoreNullValues(true)
                .Ignore(dest => dest.MainImage)
                .Ignore(dest => dest.SubImages);

            TypeAdapterConfig<CategoryUpdateRequest, Category>.NewConfig()
                .IgnoreNullValues(true)
                .Ignore(dest => dest.Translations);

            TypeAdapterConfig<BrandUpdateRequest, Brand>.NewConfig()
                .IgnoreNullValues(true)
                .Ignore(dest => dest.Logo)
                .Ignore(dest => dest.BrandTranslations);

            TypeAdapterConfig<Cart, CartResponse>.NewConfig()
                .Map(dest => dest.ProductName, source => source.Product.Translations.Where(
                    t => t.Language == CultureInfo.CurrentCulture.Name).Select(
                    t => t.Name).FirstOrDefault())
                .Map(dest => dest.Price , source => source.Product.Price)
                .Map(dest => dest.Disscount , source => source.Product.Discount)
                .Map(dest => dest.ProductImage , source => MapContext.Current.GetService<IUrlService>()
                .GetImageUrl(source.Product.MainImage));

            TypeAdapterConfig<OrderItem, OrderItemResponse>.NewConfig()
                .Map(dest => dest.ProductName, src => src.Product.Translations.FirstOrDefault().Name);

            TypeAdapterConfig<ApplicationUser, UserListResponse>.NewConfig()
                .Map(dest => dest.IsBlocked, src =>
                   src.LockoutEnd != null && src.LockoutEnd > DateTimeOffset.UtcNow);

            TypeAdapterConfig<ApplicationUser, UserDetailsResponse>.NewConfig()
                .Map(dest => dest.IsBlocked, src =>
                   src.LockoutEnd != null && src.LockoutEnd > DateTimeOffset.UtcNow);
        }
    }
}
