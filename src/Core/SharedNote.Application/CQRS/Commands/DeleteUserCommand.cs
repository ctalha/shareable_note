using MediatR;
using SharedNote.Application.BaseResponse;
using SharedNote.Application.Interfaces.Common;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace SharedNote.Application.CQRS.Commands
{
    public class DeleteUserCommand : IRequest<IResponse>
    {
        [Display(Name = "Kullanıcı")]
        [Required(ErrorMessage = "Kullanıcı bilgisi gereklidir.")]
        public string UserId { get; set; }
        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, IResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<IResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
                if (user == null) return new ErrorResponse("Kullanıcı Bulunamadı.", 404);
                await _unitOfWork.UserRepository.DeleteAsync(user);
                await _unitOfWork.CompleteAsync();
                return new SuccessResponse("Kullanıcı Silindi.", 204);
            }
        }
    }
}
