namespace EBev.Application
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(string message)
        {
            Message = message;
        }
        public string Message { get; }
    }
}
