using KASHOP.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace KASHOP.PL.Extensions
{
    public static class DataBaseExtensions
    {
        public static IServiceCollection AddDataBaseServices(this IServiceCollection Services,
            IConfiguration Configuration)
        {
            Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefalutConnection"));
            });

            return Services;
        }
    }
}
