using AutoMapper;
using MediatR;
using SharedNote.Application.Dtos;
using SharedNote.Application.Interfaces.Common;
using SharedNote.Application.Interfaces.Repositories;
using SharedNote.Application.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SharedNote.Domain.Entites;
using SharedNote.Application.Exceptions;

namespace SharedNote.Application.CQRS.Queries
{
    public class GetAllCollegeQueries : IRequest<IDataResponse<List<CollegeDto>>>
    {
        public class GetAllCollegeQueriesHandler : IRequestHandler<GetAllCollegeQueries, IDataResponse<List<CollegeDto>>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ICacheManager _cache;
            public GetAllCollegeQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheManager cache)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _cache = cache;
            }
            public async Task<IDataResponse<List<CollegeDto>>> Handle(GetAllCollegeQueries request, CancellationToken cancellationToken)
            {

                var result = await _cache.GetOrCreateAsync("college_get_all", async () =>
                {
                    return await _unitOfWork.CollegeRepository.GetAllAsync();
                });
                if (result == null)
                {
                    return new ErrorDataResponse<List<CollegeDto>>(null,"Üniversiteler Bulunamadı",404);
                }
                var dest = _mapper.Map<List<CollegeDto>>(result);
                return new SuccessDataResponse<List<CollegeDto>>(dest, "Tüm üniversiteler getirildi",200);
            }
        }
    }
}
