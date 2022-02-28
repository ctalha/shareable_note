using MediatR;
using SharedNote.Application.BaseResponse;
using SharedNote.Application.Helpers.File;
using SharedNote.Application.Interfaces.Common;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace SharedNote.Application.CQRS.Queries
{
    public class GetFileDocumentQueries : IRequest<IDataResponse<FileResponseModel>>
    {
        [Required(ErrorMessage = "Dosya Id'si bilgisi boş olamaz")]
        [Display(Name = "Dosya Id")]
        public int Id { get; set; }

        public class GetFileDocumentQueriesHandler : IRequestHandler<GetFileDocumentQueries, IDataResponse<FileResponseModel>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetFileDocumentQueriesHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<IDataResponse<FileResponseModel>> Handle(GetFileDocumentQueries request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.FileDocumentRespository.GetByIdAsync(request.Id);
                if (result == null) return new ErrorDataResponse<FileResponseModel>(null, "Dosya Bulunamadı.", 404);
                var data = await FileHelper.DownloadFileAsync(result);
                return new SuccessDataResponse<FileResponseModel>(data, 200);
            }
        }
    }
}
