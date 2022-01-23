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
            private readonly ICacheManager _cacheManager;
            public GetCollegeWithDepartmentByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheManager cacheManager)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _cacheManager = cacheManager;
            }
            public async Task<IDataResponse<CollegeWithDepartmentDto>> Handle(GetCollegeWithDepartmentByIdQueries request, CancellationToken cancellationToken)
            {
                var result = await _cacheManager.GetOrCreateAsync("college_get_with_department_by_"+request.Id , async () =>
                {
                    return await _unitOfWork.collegeRepository.GetCollegesWithDepartmentByIdAsync(request.Id);
                });
                //var result = await _unitOfWork.collegeRepository.GetCollegesWithDepartmentByIdAsync(request.Id);
                var dest = _mapper.Map<CollegeWithDepartmentDto>(result);
                return new SuccessDataResponse<CollegeWithDepartmentDto>(dest);
            }
        }
    }
}
