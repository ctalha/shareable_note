namespace SharedNote.Application.BaseResponse
{
    public interface IDataResponse<T> : IResponse
    {
        public T Data { get; set; }
    }
}
