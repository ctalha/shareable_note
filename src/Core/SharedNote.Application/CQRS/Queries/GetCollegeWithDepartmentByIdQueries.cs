using AutoMapper;
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
    public class GetCollegeWithDepartmentByIdQueries : IRequest<IDataResponse<CollegeWithDepartmentDto>>
    {
        public int Id { get; set; }
        public class GetCollegeWithDepartmentByIdHandler : IRequestHandler<GetCollegeWithDepartmentByIdQueries, IDataResponse<CollegeWithDepartmentDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private IMapper _mapper;
            public GetCollegeWithDepartmentByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<IDataResponse<CollegeWithDepartmentDto>> Handle(GetCollegeWithDepartmentByIdQueries request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.collegeRepository.GetCollegesWithDepartmentByIdAsync(request.Id);
                var dest = _mapper.Map<CollegeWithDepartmentDto>(result);
                return new SuccessDataResponse<CollegeWithDepartmentDto>(dest);
            }
        }
    }
}
