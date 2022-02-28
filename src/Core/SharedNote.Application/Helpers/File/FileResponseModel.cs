namespace SharedNote.Application.Helpers.File
{
    public class FileResponseModel
    {
        public byte[] FileContext { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public FileResponseModel(byte[] fileContext, string contentType, string fileName)
        {
            FileContext = fileContext;
            ContentType = contentType;
            FileName = fileName;
        }
    }
}
