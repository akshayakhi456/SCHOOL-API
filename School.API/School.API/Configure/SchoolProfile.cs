using AutoMapper;
using School.API.Core.Entities;
using School.API.Core.Models.ExamRequestResponseModel;
using School.API.Core.Models.SettingRequestResponseModel;
using School.API.Core.Models.StudentMapTeacherRequestResponseModel;
using School.API.Core.Models.SubjectRequestResponseModel;

namespace School.API.Configure
{
    public class SchoolProfile : Profile
    {
        public SchoolProfile() {
            CreateMap<MarksRequestModel, StudentMarks>().ReverseMap();
            CreateMap<StudentMapClassRequestModel, StudentClassSection>();
            CreateMap<SectionRequestModel, Section>();
            CreateMap<SubjectRequestModel, ClassAssignSubjectTeacher>();
            CreateMap<ClassWiseSubjectRequestModel, ClassWiseSubjects>();
            CreateMap<ExamRequestModel, ExamSubjectSchedule>();
        }
    }
}
