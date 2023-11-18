namespace Net6.Models
{
    public class PagingRequest
    {
        public int PageSize { get; set; }
        public int Page { get; set; }

        public int Skip => (Page - 1) * PageSize;
        public int Take => PageSize;
    }
}
