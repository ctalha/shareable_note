using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedNote.Application.BaseResponse;
using SharedNote.Application.Dtos;
using SharedNote.Application.Interfaces.Common;
using SharedNote.Domain.Entites;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SharedNote.Application.CQRS.Queries
{
    public class GetAllUserQueries : IRequest<IDataResponse<IQueryable<UserDto>>>
    {
        public class GetAllUserQueriesHandler : IRequestHandler<GetAllUserQueries, IDataResponse<IQueryable<UserDto>>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly UserManager<User> _userManager;

            public GetAllUserQueriesHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
            {
                _userManager = userManager;
                _unitOfWork = unitOfWork;
            }
            public async Task<IDataResponse<IQueryable<UserDto>>> Handle(GetAllUserQueries request, CancellationToken cancellationToken)
            {
                var result = _unitOfWork.UserRepository.GetAllUserWithRoles();
                if (result == null) return new ErrorDataResponse<IQueryable<UserDto>>(null, "Kullanıcı Bulunamadı", 404);
                return new SuccessDataResponse<IQueryable<UserDto>>(result, "Kullanıcılar Listelendi", 200);

            }
        }
    }
}
