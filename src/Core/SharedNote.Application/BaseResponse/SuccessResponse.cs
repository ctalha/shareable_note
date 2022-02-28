namespace SharedNote.Application.BaseResponse
{
    public class SuccessResponse : IResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public SuccessResponse(string message, int statusCode)
        {
            Message = message;
            IsSuccess = true;
            StatusCode = statusCode;
        }
        public SuccessResponse()
        {

        }
    }
}
