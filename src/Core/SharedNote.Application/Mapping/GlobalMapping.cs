using AutoMapper;
using SharedNote.Application.CQRS.Commands;
using SharedNote.Application.Dtos;
using SharedNote.Application.Helpers.File;
using SharedNote.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.Mapping
{
    public class GlobalMapping : Profile
    {
        public GlobalMapping()
        {
            CreateMap<College, CollegeDto>().ReverseMap();
            CreateMap<College, CollegeWithDepartmentDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<FileDocument, AddFileDocumentCommand>()
                .ForMember(p => p.DepartmentId, opt => opt.MapFrom(s => s.DepartmentId))
                .ForMember(p => p.CourseTitle, opt => opt.MapFrom(s => s.CourseTitle)).ReverseMap();
            CreateMap<FileDocument, FileModel>()
                .ForMember(p => p.Name, opt => opt.MapFrom(s => s.Path))
                .ForMember(p => p.Length, opt => opt.MapFrom(s => s.Length))
                .ForMember(p => p.Extension, opt => opt.MapFrom(s => s.Extension))
                .ForMember(p => p.DirectoryName, opt => opt.MapFrom(s => s.DirectoryName))
                .ForMember(p => p.FullName, opt => opt.MapFrom(s => s.FullPath)).ReverseMap();
            CreateMap<RegisterUserCommand, User>()
                .ForMember(p => p.UserName, opt => opt.MapFrom(s => s.Name))
                .ForMember(p => p.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(p => p.PasswordHash, opt => opt.MapFrom(s => s.Password)).ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();

        }
    }
}
