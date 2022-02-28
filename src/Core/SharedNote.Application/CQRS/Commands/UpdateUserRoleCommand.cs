using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedNote.Application.BaseResponse;
using SharedNote.Application.Dtos;
using SharedNote.Domain.Entites;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace SharedNote.Application.CQRS.Commands
{
    public class UpdateUserRoleCommand : IRequest<IDataResponse<UserDto>>
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Id bilgisi boş olamaz.")]
        public string Id { get; set; }
        [Display(Name = "Kullanıcı ismi")]
        [Required(ErrorMessage = "Kullanıcı ismi boş olamaz.")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email bilgisi boş olamaz.")]
        [EmailAddress(ErrorMessage = "Email doğru formatta değil.")]
        public string Email { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role bilgisi boş olamaz")]
        public string Role { get; set; }
        public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, IDataResponse<UserDto>>
        {
            private readonly IMapper _mapper;
            private readonly UserManager<User> _userManager;
            private readonly RoleManager<UserRole> _roleManager;
            public UpdateUserRoleCommandHandler(IMapper mapper, UserManager<User> userManager, RoleManager<UserRole> roleManager)
            {
                _mapper = mapper;
                _userManager = userManager;
                _roleManager = roleManager;
            }
            public async Task<IDataResponse<UserDto>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var user = await _userManager.FindByIdAsync(request.Id);
                        if (user == null) return new ErrorDataResponse<UserDto>(null, "Kullanıcı Bulunamadı", 404);

                        user.Email = request.Email;
                        user.UserName = request.UserName;
                        user.Id = request.Id;

                        var result = await _userManager.UpdateAsync(user);

                        if (!result.Succeeded)
                        {
                            scope.Complete();
                            return new SuccessDataResponse<UserDto>(null, "Kullanıcı Güncellenemedi", 400);
                        }


                        var updatedUser = await _userManager.FindByIdAsync(request.Id);

                        var dest = _mapper.Map<UserDto>(updatedUser);
                        dest.Roles = request.Role;
                        if (await _userManager.IsInRoleAsync(updatedUser, request.Role))
                        {
                            scope.Complete();
                            return new SuccessDataResponse<UserDto>(dest, "Kullanıcı Güncellendi", 200);
                        }

                        var existRole = await _roleManager.FindByNameAsync(request.Role);
                        if (existRole == null)
                        {
                            scope.Dispose();
                            return new ErrorDataResponse<UserDto>(null, $@"{request.Role} adında bir role bulunmamaktadır. Kullanıcı güncellenemedi.", 400);
                        }

                        var roles = await _userManager.GetRolesAsync(updatedUser);
                        if (roles.Count > 0)
                            foreach (var role in roles)
                            {
                                await _userManager.RemoveFromRoleAsync(updatedUser, role);
                            }
                        var IsAddRole = await _userManager.AddToRoleAsync(updatedUser, request.Role);
                        if (!IsAddRole.Succeeded)
                        {
                            scope.Dispose();
                            return new ErrorDataResponse<UserDto>(null, "Kullanıcı güncellenemedi.", 400);
                        }

                        var userRole = await _userManager.GetRolesAsync(updatedUser);
                        dest.Roles = userRole[0];
                        scope.Complete();
                        return new SuccessDataResponse<UserDto>(dest, "Kullanıcı güncellendi", 200);
                    }
                    catch (Exception ex)
                    {
                        scope.Dispose();
                        return new SuccessDataResponse<UserDto>(null, $@"{ex.Message}", 400);
                    }
                }

            }
        }
    }
}
