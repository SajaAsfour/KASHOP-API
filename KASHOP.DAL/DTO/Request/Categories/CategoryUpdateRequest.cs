using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTO.Request.Categories
{
    public class CategoryUpdateRequest
    {
        public List<CategoryTranslationRequest>? Translations { get; set; }
    }
}
