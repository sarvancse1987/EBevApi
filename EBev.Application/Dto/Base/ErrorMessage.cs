namespace EBev.Application
{
    public class ErrorMessage
    {
        public string MessageId { get; set; }
        public string Message { get; set; }
        public string FriendlyMessage { get; set; }
        public string MessageType { get; set; }
        public string StackTrace { get; set; }
        public int StatusCode { get; set; }
    }
}
