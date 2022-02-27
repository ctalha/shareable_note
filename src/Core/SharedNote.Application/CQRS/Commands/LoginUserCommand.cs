using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedNote.Application.BaseResponse;
using SharedNote.Application.Dtos;
using SharedNote.Application.Security;
using SharedNote.Domain.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedNote.Application.CQRS.Commands
{
    public class LoginUserCommand : IRequest<IDataResponse<TokenDto>>
    {
        [Required(ErrorMessage ="E-mail boş olamaz")]
        [EmailAddress(ErrorMessage = "E-mail doğru formatta olmalıdır")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre boş olamaz")]
        [DataType(DataType.Password)]
        [Display(Name ="Şifre")]
        public string Password { get; set; }

        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, IDataResponse<TokenDto>>
        {
            private readonly UserManager<User> _userManager;
            private readonly ITokenHelper _tokenHelper;
            public LoginUserCommandHandler(UserManager<User> userManager,ITokenHelper tokenHelper)
            {
                _userManager = userManager;
                _tokenHelper = tokenHelper;
            }
            public async Task<IDataResponse<TokenDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    return new ErrorDataResponse<TokenDto>(null, "Kullanıcı Bulunamadı", 404);
                if (!await _userManager.CheckPasswordAsync(user, request.Password))
                    return new ErrorDataResponse<TokenDto>(null, "Email veya Şifre Hatalı", 403);
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Count <= 0) roles.Add("member");
                var jwt = _tokenHelper.CreateToken(user, roles[0]);
                return new SuccessDataResponse<TokenDto>(new TokenDto
                {
                    Token = jwt.Token,
                    Expiration = jwt.Expirations
                }, 200);
                


                
            }
        }
    }
}
