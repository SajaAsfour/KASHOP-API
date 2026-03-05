namespace KASHOP.DAL.DTO.Response
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public List<CategoryTranslationsResponse> Translations {  get; set; }
    }
}
