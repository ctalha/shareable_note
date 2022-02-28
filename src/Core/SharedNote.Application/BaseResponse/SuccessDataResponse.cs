namespace SharedNote.Application.BaseResponse
{
    public class SuccessDataResponse<T> : IDataResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public SuccessDataResponse(T data, string message, int statusCode)
        {
            Data = data;
            Message = message;
            IsSuccess = true;
            StatusCode = statusCode;
        }
        public SuccessDataResponse(T data, int statusCode) : this(data, null, statusCode)
        {
            Data = data;
        }

    }
}
