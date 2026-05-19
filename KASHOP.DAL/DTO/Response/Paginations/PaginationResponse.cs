namespace KASHOP.DAL.DTO.Response.Paginations
{
    public class PaginationResponse<T>
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int Page {  get; set; }
        public int Limit { get; set; }
        public int TotalPage => (int) Math.Ceiling((double) TotalCount / Limit);
    }
}
