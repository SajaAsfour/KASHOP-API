using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Mapping
{
    public static class MapsterConfig
    {
        public static void MapsterConfigRegister()
        {
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
                .Map(dest => dest.category_Id, source => source.Id)
                .Map(dest => dest.UserCreated , source => source.CreatedBy)
                .Map(dest => dest.Name , source => source.Translations.Where(
                    t => t.Language == CultureInfo.CurrentCulture.Name).Select(
                    t => t.Name).FirstOrDefault());

        }
    }
}
