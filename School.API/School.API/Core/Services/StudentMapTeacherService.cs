using School.API.Common;
using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.StudentMapTeacherRequestResponseModel;

namespace School.API.Core.Services
{
    public class StudentMapTeacherService : IStudentMapTeacher
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public StudentMapTeacherService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public string studentAttendance(List<StudentAttendance> studentAttendance)
        {
            //var student = _applicationDbContext.StudentAttendances.FirstOrDefault(x => x.Id == studentAttendance.Id);
                _applicationDbContext.StudentAttendances.AddRange(studentAttendance);
                _applicationDbContext.SaveChanges();
            return "Attendance Save Successfully.";
        }

        public string updateStudentAttendance(List<AttendanceRequestModel> attendanceRequestModel)
        {
            foreach (var item in attendanceRequestModel)
            {
                int? date = item.Date;
                var columnName = $"D{date}";
                var student = _applicationDbContext.StudentAttendances.Where(x => x.Id == item.Id).FirstOrDefault();
                if (student == null)
                {
                    throw new EntityInvalidException("Attendance Update", "Record not found");
                }
                // Check if the property exists
                var property = student.GetType().GetProperty(columnName);
                if (property == null)
                {
                    throw new ArgumentException($"Property '{columnName}' not found in StudentAttendance entity.");
                }

                // Ensure 'AttendanceStatus' matches the type of the property
                var attendanceStatusType = property.PropertyType;
                if (item.AttendanceStatus.GetType() != attendanceStatusType)
                {
                    throw new ArgumentException($"Attendance status type '{item.AttendanceStatus.GetType()}' doesn't match the type of property '{attendanceStatusType}'.");
                }

                property.SetValue(student, item.AttendanceStatus);
                _applicationDbContext.SaveChanges();
            }
            return "Attendance Updated Successfully.";
        }

        public List<StudentAttendance> getStudentAttendance(AttendanceRequestModel attendanceRequestModel)
        {

            var student = _applicationDbContext.StudentAttendances.Where(x => x.Month == attendanceRequestModel.Month 
            && x.Year == attendanceRequestModel.Year
            && x.ClassName == attendanceRequestModel.ClassName
            && (x.Section == attendanceRequestModel.Section || String.IsNullOrEmpty(attendanceRequestModel.Section))).ToList();
                
            return student;
        }

        public string studentAssignSection(List<StudentClassSection> studentClassSections)
        {
            foreach (var studentClassSection in studentClassSections)
            {
                var sectionWithStudent = _applicationDbContext.StudentClassSections.FirstOrDefault(x =>
                x.ClassName == studentClassSection.ClassName
                && x.Section == studentClassSection.Section
                && x.SId == studentClassSection.SId);
                if (sectionWithStudent is StudentClassSection)
                {
                    sectionWithStudent.Section = studentClassSection.Section;
                    sectionWithStudent.RollNo = studentClassSection.RollNo;
                    _applicationDbContext.SaveChanges();
                }
                else
                {
                    _applicationDbContext.StudentClassSections.Add(studentClassSection);
                    _applicationDbContext.SaveChanges();
                }
            }
            return "Student RollNo And Section Assigned Successfully";
        }

        public List<StudentClassSection> getListOfStudent(string ClassName, string section, int academicYearId)
        {
            var result = _applicationDbContext.StudentClassSections.Where(x => x.ClassName == ClassName 
            && (x.Section == section || section == null)
            && x.AcademicYear == academicYearId).ToList();
            return result;
        }

        public List<StudentAttendance> GetStudentAttendanceByMonthYear(StudentAttendanceMonthlyRequestModel attendanceRequestModel)
        {
            // Parse start and end months
            int startMonth = attendanceRequestModel.StartMonth;
            int endMonth = attendanceRequestModel.EndMonth ?? 0;

            // Fetch all records matching the SId
            var allStudentAttendance = _applicationDbContext.StudentAttendances
                .Where(x => x.SId == attendanceRequestModel.SId)
                .ToList();

            // Filter records based on the specified month and year range
            var student = allStudentAttendance
                        .Where(x =>
                            ((int.Parse(x.Month) >= startMonth && int.Parse(x.Month) <= endMonth) && x.Year == attendanceRequestModel.StartYear) ||
                            (int.Parse(x.Month) == startMonth && x.Year == attendanceRequestModel.StartYear))
                        .ToList();

            return student;
        }
    }
}
