namespace Ecom.API.Helper
{
    public class ResponeAPI
    {
        public ResponeAPI(int statusCode, string? message=null)
        {
            StatusCode = statusCode;
            Message = message?? GetMessageFromStatusCode(StatusCode);
        }
        private string GetMessageFromStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Done",
                400 => "Bad Request",
                401 => "Unauthorized",
                500 => "Server Error",
                _ => "Null"
            };
        }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
