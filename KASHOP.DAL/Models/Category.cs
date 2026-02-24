namespace KASHOP.DAL.Models
{
    public class Category
    {
        public int Id { get; set; }
        public List<CategoryTranslation> Translations { get; set; }
    }
}
