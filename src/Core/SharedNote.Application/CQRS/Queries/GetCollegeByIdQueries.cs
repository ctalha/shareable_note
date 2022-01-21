﻿using AutoMapper;
using MediatR;
using SharedNote.Application.Dtos;
using SharedNote.Application.Interfaces.Common;
using SharedNote.Application.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedNote.Application.CQRS.Queries
{
    public class GetCollegeByIdQueries : IRequest<IDataResponse<CollegeDto>>
    {
        public int Id { get; set; }
        public class GetCollegeByIdHandler : IRequestHandler<GetCollegeByIdQueries, IDataResponse<CollegeDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private IMapper _mapper;
            public GetCollegeByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<IDataResponse<CollegeDto>> Handle(GetCollegeByIdQueries request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.collegeRepository.GetByIdAsync(request.Id);
                var dest = _mapper.Map<CollegeDto>(result);
                return new SuccessDataResponse<CollegeDto>(dest, "İstenilen üniversite getirildi");
            }
        }
    }
}
