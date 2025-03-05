namespace Bookly.APIs.DTOs
{
    public class DeletedMessageDto
    {
        public string Message { get; set; }

        public DeletedMessageDto(string message)
        {
            Message = message;
        }
    }
}
