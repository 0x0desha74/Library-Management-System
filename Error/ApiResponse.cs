
namespace Bookly.APIs.Error
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode, string message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return StatusCode switch
            {
                400 => "A bad request, you have made",
                404 => "Resource was not found",
                401 => "Authorized, you are not",
                500 => "Errors are the bath to dark side.Errors leads to anger. anger leads to hate. Hates leads to career change.",
                _=>null
            };
        }
    }
}
