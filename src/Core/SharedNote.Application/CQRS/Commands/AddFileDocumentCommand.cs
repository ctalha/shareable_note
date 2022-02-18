using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SharedNote.Application.Helpers.File;
using SharedNote.Application.Interfaces.Common;
using SharedNote.Application.BaseResponse;
using SharedNote.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace SharedNote.Application.CQRS.Commands
{
    public class AddFileDocumentCommand : IRequest<IResponse>
    {
        public IFormFile File { get; set; }
        public string CourseTitle { get; set; }
        public int DepartmentId { get; set; }
        public class AddFileDocumentCommandHandler : IRequestHandler<AddFileDocumentCommand, IResponse>
        {
            private readonly IHostingEnvironment _env;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ICacheManager _cacheManager;
            public AddFileDocumentCommandHandler(IHostingEnvironment env, IUnitOfWork unitOfWork, IMapper mapper, ICacheManager cacheManager)
            {
                _env = env;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _cacheManager = cacheManager;
            }
            public async Task<IResponse> Handle(AddFileDocumentCommand request, CancellationToken cancellationToken)
            {

                var fileModel = FileHelper.Upload(request.File,_env);

                var dest = _mapper.Map(fileModel, _mapper.Map<AddFileDocumentCommand, FileDocument>(request));
                dest.DocumentTitle = request.File.FileName;
                dest.CreateDate = DateTime.Now;
                dest.ContentType = request.File.ContentType;

                await _unitOfWork.FileDocumentRespository.AddAsync(dest);
                var result = await _unitOfWork.CompleteAsync();

                await Task.CompletedTask;
                if (result)
                {
                    _cacheManager.RemoveSameCache("FileDocument");
                    return new SuccessResponse("Dosya Yüklendi",200);
                }
                return new ErrorResponse("Dosya Yüklenemedi",200);
                
            }
        }
    }
}
