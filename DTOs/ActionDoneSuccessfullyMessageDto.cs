namespace Bookly.APIs.DTOs
{
    public class ActionDoneSuccessfullyMessageDto
    {
        public string Message { get; set; }

        public ActionDoneSuccessfullyMessageDto(string message)
        {
            Message = message;
        }
    }
}
