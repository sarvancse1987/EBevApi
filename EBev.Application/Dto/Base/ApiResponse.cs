namespace EBev.Application
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Result { get; set; }
        public ApiErrorResponse? Error { get; set; }
    }
}
