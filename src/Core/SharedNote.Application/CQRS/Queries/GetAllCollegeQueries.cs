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

namespace SharedNote.Application.CQRS.Queries
{
    public class GetAllCollegeQueries : IRequest<IDataResponse<List<CollegeDto>>>
    {
        public class GetAllCollegeQueriesHandler : IRequestHandler<GetAllCollegeQueries, IDataResponse<List<CollegeDto>>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public GetAllCollegeQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<IDataResponse<List<CollegeDto>>> Handle(GetAllCollegeQueries request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.collegeRepository.GetAllAsync();
                var dest = _mapper.Map<List<CollegeDto>>(result);
                return new SuccessDataResponse<List<CollegeDto>>(dest, "Tüm üniversiteler getirildi");
            }
        }
    }
}
