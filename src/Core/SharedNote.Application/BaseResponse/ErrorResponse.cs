namespace SharedNote.Application.BaseResponse
{
    public class ErrorResponse : IResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public ErrorResponse(string message, int statusCode)
        {
            Message = message;
            IsSuccess = false;
            StatusCode = statusCode;
        }
    }
}
