using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SharedNote.Application.BaseResponse;
using SharedNote.Application.Helpers.File;
using SharedNote.Application.Interfaces.Common;
using SharedNote.Domain.Entites;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace SharedNote.Application.CQRS.Commands
{
    public class AddFileDocumentCommand : IRequest<IResponse>
    {
        //[Display(Name = "Dosya")]
        //[Required(ErrorMessage = "Yüklenecek dosya gereklidir.")]
        //  [FileExtensions(Extensions = "jpg,png,pdf,odt,txt,7z,zip",ErrorMessage = "Desteklenmeyen dosya türü, lütfen .jpg, .png, .pdf, .odt, .txt, .7z, .zip dosya türü kullanın")]

        public IFormFile File { get; set; }

        [Display(Name = "Kurs")]
        [Required(ErrorMessage = "Kurs isim bilgisi gereklidir.")]
        public string CourseTitle { get; set; }

        [Display(Name = "Bölüm")]
        [Required(ErrorMessage = "Bölüm bilgisi gereklidir.")]
        public int DepartmentId { get; set; }

#nullable enable
        public string? UserId { get; set; }
#nullable disable
        public class AddFileDocumentCommandHandler : IRequestHandler<AddFileDocumentCommand, IResponse>
        {
            private readonly IHostingEnvironment _env;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ICacheManager _cacheManager;
            private readonly UserManager<User> _userManager;
            public AddFileDocumentCommandHandler(IHostingEnvironment env, IUnitOfWork unitOfWork, IMapper mapper, ICacheManager cacheManager, UserManager<User> userManager)
            {
                _env = env;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _cacheManager = cacheManager;
                _userManager = userManager;
            }

            public async Task<IResponse> Handle(AddFileDocumentCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null) return new ErrorResponse("Email veya şifre hatalıdır.", 404);

                var fileModel = FileHelper.Upload(request.File, _env);

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
                    return new SuccessResponse("Dosya Yüklendi", 200);
                }
                return new ErrorResponse("Dosya Yüklenemedi", 200);

            }
        }
    }
}
