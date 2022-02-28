using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedNote.Application.BaseResponse;
using SharedNote.Application.Dtos;
using SharedNote.Application.Security;
using SharedNote.Domain.Entites;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace SharedNote.Application.CQRS.Commands
{
    public class RegisterUserCommand : IRequest<IDataResponse<TokenDto>>
    {
        [Display(Name = "İsim")]
        [Required(ErrorMessage = "Kullanıcı ismi boş olamaz.")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email boş olamaz.")]
        [EmailAddress(ErrorMessage = "Email adresi doğru formatta değil.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Şifre boş olamaz")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        public class CreateUserCommandHander : IRequestHandler<RegisterUserCommand, IDataResponse<TokenDto>>
        {
            private readonly UserManager<User> _userManager;
            private readonly ITokenHelper _tokenHelper;
            private readonly IMapper _mapper;
            private readonly RoleManager<UserRole> _roleManager;

            public CreateUserCommandHander(UserManager<User> userManager, ITokenHelper tokenHelper, IMapper mapper, RoleManager<UserRole> roleManager)
            {
                _userManager = userManager;
                _tokenHelper = tokenHelper;
                _mapper = mapper;
                _roleManager = roleManager;
            }

            public async Task<IDataResponse<TokenDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                if (await _userManager.FindByEmailAsync(request.Email) != null)
                    return new ErrorDataResponse<TokenDto>(null, "Bu Email Kullanılıyor.", 409);

                if (!await _roleManager.RoleExistsAsync("member"))
                    await _roleManager.CreateAsync(new UserRole { Name = "member" });

                var dest = _mapper.Map<User>(request);
                dest.CreateDate = DateTime.Now;
                var result = await _userManager.CreateAsync(dest, request.Password);

                if (!result.Succeeded)
                    return new ErrorDataResponse<TokenDto>(null, "Kullanıcı kayıt edilemedi.", 500);

                await _userManager.AddToRoleAsync(dest, "member");

                var jwt = _tokenHelper.CreateToken(dest, "member");

                return new SuccessDataResponse<TokenDto>(new TokenDto
                {
                    Token = jwt.Token,
                    Expiration = jwt.Expirations
                }, "Kullancı kayıt edildi", 201);

            }
        }
    }
}
