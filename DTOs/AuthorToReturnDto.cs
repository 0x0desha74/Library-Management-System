namespace Bookly.APIs.DTOs
{
    public class AuthorToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly? DeathDate { get; set; }
        public ICollection<string> Books { get; set; }
    }
}
