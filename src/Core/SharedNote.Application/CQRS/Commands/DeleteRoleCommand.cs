using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedNote.Application.BaseResponse;
using SharedNote.Domain.Entites;
using System.Threading;
using System.Threading.Tasks;

namespace SharedNote.Application.CQRS.Commands
{
    public class DeleteRoleCommand : IRequest<IResponse>
    {
        public string Id { get; set; }
        public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, IResponse>
        {
            private readonly RoleManager<UserRole> _roleManager;
            private readonly UserManager<User> _userManager;
            public DeleteRoleCommandHandler(RoleManager<UserRole> roleManager, UserManager<User> userManager)
            {
                _roleManager = roleManager;
                _userManager = userManager;
            }
            public async Task<IResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByIdAsync(request.Id);
                if (role == null) return new ErrorResponse("Role bulunamadı, role silme işlemi başarız oldu.", 404);
                var users = await _userManager.GetUsersInRoleAsync(role.Name);
                if (users.Count > 0)
                {
                    foreach (var user in users)
                    {
                        await _userManager.AddToRoleAsync(user, "member");
                    }
                }
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded) return new SuccessResponse("Role başarılı bir şekilde silindi.", 204);
                return new ErrorResponse("Role silinemedi.", 400);

            }
        }
    }
}
