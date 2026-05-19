using System.ComponentModel.DataAnnotations;

namespace KASHOP.DAL.DTO.Request.Reviews
{
    public class AddReviewRequest
    {
        public int ProductId { get; set; }
        public string Comment { get; set; }
        [Range(1,5)]
        public int Rate { get; set; }
    }
}
