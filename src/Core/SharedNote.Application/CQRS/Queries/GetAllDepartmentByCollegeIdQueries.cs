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
    public class GetAllDepartmentByCollegeIdQueries : IRequest<IDataResponse<List<DepartmentDto>>>
    {
        public int CollegeId { get; set; }
        public class GetAllDepartmentByCollegeIdQueriesHandler : IRequestHandler<GetAllDepartmentByCollegeIdQueries, IDataResponse<List<DepartmentDto>>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public GetAllDepartmentByCollegeIdQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<IDataResponse<List<DepartmentDto>>> Handle(GetAllDepartmentByCollegeIdQueries request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.departmentRepository.GetAllDepartmentsByCollegeIdAsync(request.CollegeId);
                return new SuccessDataResponse<List<DepartmentDto>>(_mapper.Map<List<DepartmentDto>>(result), "Bölümler getirildi");
            }
        }
    }
}
