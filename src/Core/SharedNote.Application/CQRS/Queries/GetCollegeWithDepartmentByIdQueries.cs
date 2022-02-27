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
using System.ComponentModel.DataAnnotations;

namespace SharedNote.Application.CQRS.Queries
{
    public class GetCollegeWithDepartmentByIdQueries : IRequest<IDataResponse<CollegeWithDepartmentDto>>
    {
        [Required(ErrorMessage = "Bölüm Id'si bilgisi boş olamaz")]
        [Display(Name = "Bölüm Id")]
        public int Id { get; set; }
        public class GetCollegeWithDepartmentByIdHandler : IRequestHandler<GetCollegeWithDepartmentByIdQueries, IDataResponse<CollegeWithDepartmentDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
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
                    return await _unitOfWork.CollegeRepository.GetCollegesWithDepartmentByIdAsync(request.Id);
                });
                if (result == null)
                {
                    return new ErrorDataResponse<CollegeWithDepartmentDto>(null, "Üniversite ve Bölümleri Getirilemedi",404);
                }
                var dest = _mapper.Map<CollegeWithDepartmentDto>(result);
                return new SuccessDataResponse<CollegeWithDepartmentDto>(dest, "Üniversite ve Bölümleri Listelendi",200);
            }
        }
    }
}
