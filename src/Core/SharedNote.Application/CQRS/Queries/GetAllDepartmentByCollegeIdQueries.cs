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
using SharedNote.Application.Exceptions;

namespace SharedNote.Application.CQRS.Queries
{
    public class GetAllDepartmentByCollegeIdQueries : IRequest<IDataResponse<List<DepartmentDto>>>
    {
        public int CollegeId { get; set; }
        public class GetAllDepartmentByCollegeIdQueriesHandler : IRequestHandler<GetAllDepartmentByCollegeIdQueries, IDataResponse<List<DepartmentDto>>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ICacheManager _cacheManager;
            public GetAllDepartmentByCollegeIdQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheManager cacheManager)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _cacheManager = cacheManager;
            }
            public async Task<IDataResponse<List<DepartmentDto>>> Handle(GetAllDepartmentByCollegeIdQueries request, CancellationToken cancellationToken)
            {
                var result = await _cacheManager.GetOrCreateAsync("department_get_all_by_"+request.CollegeId, async () =>
                {
                    return await _unitOfWork.DepartmentRepository.GetAllDepartmentsByCollegeIdAsync(request.CollegeId);
                });
                if (result.Count <= 0)
                {
                    return new ErrorDataResponse<List<DepartmentDto>>(null,"Üniversite Bölümleri Bulunamadı",404);
                }
                return new SuccessDataResponse<List<DepartmentDto>>(_mapper.Map<List<DepartmentDto>>(result), "Bölümler getirildi",200);
            }
        }
    }
}
