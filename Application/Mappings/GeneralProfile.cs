using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Student, StudentDTO>();
            CreateMap<StudentDTO, Student>();

            CreateMap<Subject, SubjectDTO>()
            .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.Name));
            CreateMap<SubjectDTO, Subject>();

            CreateMap<Teacher, TeacherDTO>();
            CreateMap<TeacherDTO, Teacher>();

            CreateMap <Program, ProgramDTO>();
            CreateMap<ProgramDTO, Program>();
        }
    }
}
