using AutoMapper;
using MediatR;
using SharedNote.Application.BaseResponse;
using SharedNote.Application.Dtos;
using SharedNote.Application.Interfaces.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace SharedNote.Application.CQRS.Queries
{
    public class GetAllDepartmentByCollegeIdQueries : IRequest<IDataResponse<List<DepartmentDto>>>
    {
        [Required(ErrorMessage = "Okul Id'si bilgisi boş olamaz")]
        [Display(Name = "Okul Id")]

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
                var result = await _cacheManager.GetOrCreateAsync("department_get_all_by_" + request.CollegeId, async () =>
                  {
                      return await _unitOfWork.DepartmentRepository.GetAllDepartmentsByCollegeIdAsync(request.CollegeId);
                  });
                if (result.Count <= 0)
                {
                    return new ErrorDataResponse<List<DepartmentDto>>(null, "Üniversite Bölümleri Bulunamadı", 404);
                }
                return new SuccessDataResponse<List<DepartmentDto>>(_mapper.Map<List<DepartmentDto>>(result), "Bölümler getirildi", 200);
            }
        }
    }
}
