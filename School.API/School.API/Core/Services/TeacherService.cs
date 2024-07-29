using AutoMapper;
using Microsoft.AspNetCore.Identity;
using School.API.Common;
using School.API.Core.DbContext;
using School.API.Core.Dtos;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.AuthUserRequestResponseModel;
using School.API.Core.Models.TeacherRequestResponseModel;
using School.API.Core.OtherObjects;
using School.API.Core.UtilityServices.interfaces;
using System.Text;

namespace School.API.Core.Services
{
    public class TeacherService : ITeacher
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IAuthService _authService;
        private readonly ISendMail _sendMail;
        public TeacherService(UserManager<ApplicationUser> userManager,
            IAuthService authService,
            ApplicationDbContext applicationDbContext,
            ISendMail sendMail)
        {
            _applicationDbContext = applicationDbContext; 
            _userManager = userManager;
            _authService = authService;
            _sendMail = sendMail;
        }

        public async Task<string> create(AddTeacherRequest addTeacherRequest)
        {
            _applicationDbContext.TeacherDetails.Add(addTeacherRequest.TeacherDetails);
            _applicationDbContext.SaveChanges();
            if (addTeacherRequest.TeacherExperience.Count() > 0)
            {
                foreach (var item in addTeacherRequest.TeacherExperience)
                {
                    item.EmpId = addTeacherRequest.TeacherDetails.Id.ToString();
                }
                _applicationDbContext.TeacherExperiences.AddRange(addTeacherRequest.TeacherExperience);
                _applicationDbContext.SaveChanges();
            }
            await createUserAccount(addTeacherRequest.TeacherDetails);
            return "Teacher Detail saved successfully.";
        }

        public string delete(int id)
        {
            throw new NotImplementedException();
        }

        public string employmentApproval(int id)
        {
            var employee = _applicationDbContext.TeacherDetails.FirstOrDefault(x => x.Id == id);
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
            var detail = _applicationDbContext.TeacherDetails.FirstOrDefault(x => x.Id.Equals(id));
            var experience = _applicationDbContext.TeacherExperiences.Where(x => x.EmpId.Equals(id.ToString())).ToList();
            AddTeacherRequest teacher = new AddTeacherRequest();
            teacher.TeacherDetails = detail;
            teacher.TeacherExperience = experience;
            return teacher;
        }

        public string update(AddTeacherRequest updateTeacherRequest)
        {
            var teacherDetail = _applicationDbContext.TeacherDetails.FirstOrDefault(x => x.Id == updateTeacherRequest.TeacherDetails.Id);
            teacherDetail.FirstName = updateTeacherRequest.TeacherDetails.FirstName;
            teacherDetail.LastName = updateTeacherRequest.TeacherDetails.LastName;
            teacherDetail.DateOfBirth = updateTeacherRequest.TeacherDetails.DateOfBirth;
            teacherDetail.PhoneNumber = updateTeacherRequest.TeacherDetails.PhoneNumber;
            teacherDetail.Qualification = updateTeacherRequest.TeacherDetails.Qualification;
            teacherDetail.PassOutYear = updateTeacherRequest.TeacherDetails.PassOutYear;
            teacherDetail.Email = updateTeacherRequest.TeacherDetails.Email;

            if (updateTeacherRequest.TeacherExperience.Count() > 0)
            {
                foreach (var item in updateTeacherRequest.TeacherExperience)
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
                        teacherExperience.SchoolName = item.SchoolName;
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

        public async Task<bool> createUserAccount(TeacherDetails teacherDetails)
        {
            var registerDTO = new RegisterDto();
            registerDTO.FirstName = teacherDetails.FirstName;
            registerDTO.LastName = teacherDetails.LastName;
            registerDTO.Email = teacherDetails.Email;
            registerDTO.UserName = teacherDetails.FirstName + "" + teacherDetails.LastName.Substring(0, 3);
            registerDTO.Password = "Password@123";
            registerDTO.Role = StaticUserRoles.TEACHER;

            await _authService.RegisterAsync(registerDTO);

            StringBuilder html = new StringBuilder();
            html.Append($"<h4>Hello {teacherDetails.FirstName}</h4><br/>");
            html.Append($"<p>Below are the credetials to login Skool UI App<p>");
            html.Append($"<p>UserName: {registerDTO.UserName}</p>");
            html.Append($"<p>Password: Password@123</p>");
            MailRequest request = new MailRequest();
            request.ToEmail = teacherDetails.Email;
            request.Subject = "Reset your Skool UI Password";
            request.Body = html.ToString();
            await _sendMail.SendEmailAsync(request);
            return true;
        }
    }
}
