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
    public class GetCollegeByIdQueries : IRequest<IDataResponse<CollegeDto>>
    {
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
                var result = await _cacheManager.GetOrCreateAsync("college_get_by_"+request.Id, async () =>
                {
                    return await _unitOfWork.CollegeRepository.GetByIdAsync(request.Id);
                });
                if (result == null)
                {
                    return new ErrorDataResponse<CollegeDto>(null, "Üniversite bulunamadı.", 404);
                }
                var dest = _mapper.Map<CollegeDto>(result);
                return new SuccessDataResponse<CollegeDto>(dest, "İstenilen üniversite getirildi",200);
            }
        }
    }
}
