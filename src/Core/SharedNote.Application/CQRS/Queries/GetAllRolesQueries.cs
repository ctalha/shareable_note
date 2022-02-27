using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedNote.Application.BaseResponse;
using SharedNote.Application.Dtos;
using SharedNote.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedNote.Application.CQRS.Queries
{
    public class GetAllRolesQueries : IRequest<IDataResponse<List<RoleResponseDto>>>
    {
        public class GetAllRolesQueriesHandler : IRequestHandler<GetAllRolesQueries, IDataResponse<List<RoleResponseDto>>>
        {
            private readonly RoleManager<UserRole> _roleManager;
            private readonly IMapper _mapper;
            public GetAllRolesQueriesHandler(RoleManager<UserRole> roleManager, IMapper mapper)
            {
                _roleManager = roleManager;
                _mapper = mapper;
            }
            public async Task<IDataResponse<List<RoleResponseDto>>> Handle(GetAllRolesQueries request, CancellationToken cancellationToken)
            {
                var dest = _mapper.Map<List<RoleResponseDto>>(await _roleManager.Roles.ToListAsync());
                return new SuccessDataResponse<List<RoleResponseDto>>(dest, "Kullanıcı rolleri listelendi.", 200);
            }
        }
    }
}
