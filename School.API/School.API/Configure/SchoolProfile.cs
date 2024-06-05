using AutoMapper;
using School.API.Core.Entities;
using School.API.Core.Models.SubjectRequestResponseModel;

namespace School.API.Configure
{
    public class SchoolProfile : Profile
    {
        public SchoolProfile() {
            CreateMap<ClassAssignSubjectTeacher, SubjectRequestModel>();
            CreateMap<ClassWiseSubjectRequestModel, ClassWiseSubjects>();
        }
    }
}
