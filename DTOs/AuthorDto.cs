namespace Bookly.APIs.DTOs
{
    public class AuthorDto
    {
        public string Name { get; set; }
        public string? Bio { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly? DeathDate { get; set; }
    }
}
