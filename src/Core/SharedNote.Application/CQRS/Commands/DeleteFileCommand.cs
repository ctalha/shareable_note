using MediatR;
using SharedNote.Application.BaseResponse;
using SharedNote.Application.Helpers.File;
using SharedNote.Application.Interfaces.Common;
using System.Threading;
using System.Threading.Tasks;

namespace SharedNote.Application.CQRS.Commands
{
    public class DeleteFileCommand : IRequest<IResponse>
    {
        public int Id { get; set; }
        public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, IResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            public DeleteFileCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<IResponse> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
            {
                var file = await _unitOfWork.FileDocumentRespository.GetByIdAsync(request.Id);
                if (file == null)
                    return new ErrorResponse("Dosya Bulunamadı", 404);
                await _unitOfWork.FileDocumentRespository.DeleteAsync(file);
                if (await _unitOfWork.CompleteAsync())
                {
                    var result = FileHelper.DeleteFile(file);
                    if (!result)
                        return new ErrorResponse("Dosya Silinemedi", 400);
                }
                return new SuccessResponse("Dosya başarılı bir şekilde silindi", 204);

            }
        }
    }
}
