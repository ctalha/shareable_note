namespace SharedNote.Application.Helpers.Logging
{
    public class LoggingResponse
    {
        public string Path { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public long ElapsedMilliseconds { get; set; }
    }
}
