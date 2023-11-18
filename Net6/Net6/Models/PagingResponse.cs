namespace Net6.Models
{
    public class PagingResponse
    {
        public Array Data { get; set; }
        public int Total { get; set; }
        public string Errors { get; set; }

        public PagingResponse(Array data, int count)
        {
            this.Data = data;
            this.Total = count;
        }

        public PagingResponse(string errors)
        {
            this.Errors = errors;
        }

        public PagingResponse()
        {

        }
    }
}
