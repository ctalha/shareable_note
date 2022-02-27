using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedNote.Application.BaseResponse;
using SharedNote.Application.Dtos;
using SharedNote.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedNote.Application.CQRS.Commands
{
    public class AddRoleCommand : IRequest<IDataResponse<RoleResponseDto>>
    {
        public string Name { get; set; }
        public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, IDataResponse<RoleResponseDto>>
        {
            private readonly RoleManager<UserRole> _roleManager;
            private readonly IMapper _mapper;
            public AddRoleCommandHandler(RoleManager<UserRole> roleManager, IMapper mapper)
            {
                _roleManager = roleManager;
                _mapper = mapper;
            }
            public async Task<IDataResponse<RoleResponseDto>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
            {
                var dest = _mapper.Map<UserRole>(request);
                var role = await _roleManager.FindByNameAsync(dest.Name);
                if (role != null) return new ErrorDataResponse<RoleResponseDto>(null,"Role kaydedilemedi, aynı isimde role bulunmaktadır", 400);
                var result =  await _roleManager.CreateAsync(dest);
                var user = await _roleManager.FindByNameAsync(request.Name);
                var resultDest = _mapper.Map<RoleResponseDto>(user);
                return new SuccessDataResponse<RoleResponseDto>(resultDest, "Role başarılı bir şekilde oluşturuldu.",201);
            }
        }
    }
}
