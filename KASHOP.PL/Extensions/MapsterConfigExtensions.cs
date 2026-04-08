using KASHOP.BLL.Mapping;
using Mapster;
using MapsterMapper;

namespace KASHOP.PL.Extensions
{
    public static class MapsterConfigExtensions
    {
        public static IServiceCollection AddMapsterConfigServices(this IServiceCollection Services)
        {
            Services.AddHttpContextAccessor();
            MapsterConfig.MapsterConfigRegister();
            Services.AddSingleton(TypeAdapterConfig.GlobalSettings);
            Services.AddScoped<IMapper, ServiceMapper>();

            return Services;
        }
    }
}
