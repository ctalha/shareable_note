using AutoMapper;
using MediatR;
using SharedNote.Application.BaseResponse;
using SharedNote.Application.Dtos;
using SharedNote.Application.Interfaces.Common;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace SharedNote.Application.CQRS.Queries
{
    public class GetCollegeByIdQueries : IRequest<IDataResponse<CollegeDto>>
    {
        [Required(ErrorMessage = "Okul Id'si bilgisi boş olamaz")]
        [Display(Name = "Okul Id")]
        public int Id { get; set; }
        public class GetCollegeByIdHandler : IRequestHandler<GetCollegeByIdQueries, IDataResponse<CollegeDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ICacheManager _cacheManager;
            public GetCollegeByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheManager cacheManager)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _cacheManager = cacheManager;
            }
            public async Task<IDataResponse<CollegeDto>> Handle(GetCollegeByIdQueries request, CancellationToken cancellationToken)
            {
                var result = await _cacheManager.GetOrCreateAsync("college_get_by_" + request.Id, async () =>
                  {
                      return await _unitOfWork.CollegeRepository.GetByIdAsync(request.Id);
                  });
                if (result == null)
                {
                    return new ErrorDataResponse<CollegeDto>(null, "Üniversite bulunamadı.", 404);
                }
                var dest = _mapper.Map<CollegeDto>(result);
                return new SuccessDataResponse<CollegeDto>(dest, "İstenilen üniversite getirildi", 200);
            }
        }
    }
}
