using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedNote.Application.BaseResponse;
using SharedNote.Application.Dtos;
using SharedNote.Domain.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedNote.Application.CQRS.Queries
{
    public class GetUserByIdQueries : IRequest<IDataResponse<UserDto>>
    {
        [Required(ErrorMessage = "Kullanıcı Id'si bilgisi boş olamaz")]
        [Display(Name = "Kullanıcı Id")]
        public string UserId { get; set; }
        public class GetUserByIdQueriesHandler : IRequestHandler<GetUserByIdQueries, IDataResponse<UserDto>>
        {
            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;
            public GetUserByIdQueriesHandler(UserManager<User> userManager, IMapper mapper)
            {
                _userManager = userManager;
                _mapper = mapper;
            }
            public async Task<IDataResponse<UserDto>> Handle(GetUserByIdQueries request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users.Where(p => p.Id == request.UserId).SingleOrDefaultAsync();
                var role = await _userManager.GetRolesAsync(user);
                var dest = _mapper.Map<UserDto>(user);
                dest.Roles = role[0];
                return new SuccessDataResponse<UserDto>(dest, "Kullanıcı başarılı bir şekilde getirildi", 200);

            }
        }
    }
}
