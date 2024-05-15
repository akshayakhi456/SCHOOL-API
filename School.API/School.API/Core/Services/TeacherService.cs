using School.API.Common;
using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.TeacherRequestResponseModel;

namespace School.API.Core.Services
{
    public class TeacherService: ITeacher
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public TeacherService(ApplicationDbContext applicationDbContext) {
            _applicationDbContext = applicationDbContext;
        }

        public string create(AddTeacherRequest addTeacherRequest)
        {
            _applicationDbContext.TeacherDetails.Add(addTeacherRequest.TeacherDetails);
            _applicationDbContext.SaveChanges();
            if (addTeacherRequest.TeacherExperience.Count() > 0)
            {
                foreach (var item in addTeacherRequest.TeacherExperience)
                {
                    item.EmpId = addTeacherRequest.TeacherDetails.id.ToString();
                }
                _applicationDbContext.TeacherExperiences.AddRange(addTeacherRequest.TeacherExperience);
                _applicationDbContext.SaveChanges();
            }
            return "Teacher Detail saved successfully.";
        }

        public string delete(int id)
        {
            throw new NotImplementedException();
        }

        public string employmentApproval(int id)
        {
            var employee = _applicationDbContext.TeacherDetails.FirstOrDefault(x => x.id == id);
            var maxEmployeeId = _applicationDbContext.TeacherDetails.OrderByDescending(e => e.EmpId).FirstOrDefault();
            if (employee == null)
            {
                throw new EntityInvalidException("Employee Approval", "Employee not exist");
            }

            throw new NotImplementedException();
        }

        public List<TeacherDetails> getTeacherDetails()
        {            
            return _applicationDbContext.TeacherDetails.ToList();
        }

        public AddTeacherRequest getTeacherById(int id)
        {
            var detail = _applicationDbContext.TeacherDetails.FirstOrDefault(x => x.id.Equals(id));
            var experience = _applicationDbContext.TeacherExperiences.Where(x => x.EmpId.Equals(id)).ToList();
            AddTeacherRequest teacher = new AddTeacherRequest();
            teacher.TeacherDetails = detail;
            teacher.TeacherExperience = experience;
            return teacher;
        }

        public string update(AddTeacherRequest updateTeacherRequest)
        {
            var teacherDetail = _applicationDbContext.TeacherDetails.FirstOrDefault(x => x.id == updateTeacherRequest.TeacherDetails.id);
            teacherDetail.FirstName = updateTeacherRequest.TeacherDetails.FirstName;
            teacherDetail.LastName = updateTeacherRequest.TeacherDetails.LastName;
            teacherDetail.DateOfBirth = updateTeacherRequest.TeacherDetails.DateOfBirth;
            teacherDetail.PhoneNumber = updateTeacherRequest.TeacherDetails.PhoneNumber;
            teacherDetail.Qualification = updateTeacherRequest.TeacherDetails.Qualification;
            teacherDetail.PassOutYear = updateTeacherRequest.TeacherDetails.PassOutYear;
            teacherDetail.Email = updateTeacherRequest.TeacherDetails.Email;

            if(updateTeacherRequest.TeacherExperience.Count() > 0)
            {
                foreach(var item in updateTeacherRequest.TeacherExperience)
                {
                    var teacherExperience = _applicationDbContext.TeacherExperiences.FirstOrDefault(x => x.Id == item.Id);

                    if (teacherExperience == null)
                    {
                        throw new EntityInvalidException("Update Teacher", "Teacher Experience Record not found");
                    }

                    if (item.Id == 0)
                    {
                        _applicationDbContext.TeacherExperiences.Add(item);
                        _applicationDbContext.SaveChanges();
                    }
                    else if (item.Status)
                    {
                        teacherExperience.SchooolName = item.SchooolName;
                        teacherExperience.StartEndDate = item.StartEndDate;
                        teacherExperience.Designation = item.Designation;
                        _applicationDbContext.SaveChanges();
                    }
                    else
                    {
                        _applicationDbContext.TeacherExperiences.Remove(item);
                        _applicationDbContext.SaveChanges();
                    }
                }
            }
            return "Teacher Details Updated Successfully";
        }
        
        public string incrementEmpId(string ExistingId)
        {
            string code = ExistingId ?? "E000";

            // Parse the numeric part
            string numericPart = code.Substring(1);
            int number = int.Parse(numericPart);

            // Increment the number
            number++;

            // Format it back to the original string format
            string newCode = "E" + number.ToString("000");

            return newCode;

        }
    }
}
