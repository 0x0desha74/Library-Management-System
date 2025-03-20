namespace Bookly.APIs.DTOs
{
    public class FavoriteToReturnDto
    {
        public int BookId { get; set; }
        public string Title{ get; set; }
        public string Author{ get; set; }
        public DateTime CreateAt{ get; set; }
    }
}
