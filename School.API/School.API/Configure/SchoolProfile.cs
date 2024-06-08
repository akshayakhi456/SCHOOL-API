using AutoMapper;
using School.API.Core.Entities;
using School.API.Core.Models.ExamRequestResponseModel;
using School.API.Core.Models.SubjectRequestResponseModel;

namespace School.API.Configure
{
    public class SchoolProfile : Profile
    {
        public SchoolProfile() {
            CreateMap<SubjectRequestModel, ClassAssignSubjectTeacher>();
            CreateMap<ClassWiseSubjectRequestModel, ClassWiseSubjects>();
            CreateMap<ExamRequestModel, ExamSubjectSchedule>();
        }
    }
}
