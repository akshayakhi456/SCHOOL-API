using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.API.Common;
using School.API.Core.DbContext;
using School.API.Core.Dtos;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.AuthUserRequestResponseModel;
using School.API.Core.Models.StudentRequestModel;
using School.API.Core.OtherObjects;
using System.Text;
using System;
using School.API.Core.UtilityServices;
using School.API.Core.UtilityServices.interfaces;
using System.Linq;

namespace School.API.Core.Services
{
    public class StudentService : IStudent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IAuthService _authService;
        private readonly ISendMail _sendMail;
        public StudentService(UserManager<ApplicationUser> userManager, IAuthService authService, ApplicationDbContext applicationDbContext, ISendMail sendMail) {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _authService = authService;
            _sendMail = sendMail;
        }
        public async Task<bool> create(StudentGuardianRequest student)
        {
            var res = _applicationDbContext.Students.Add(student.students);
            await _applicationDbContext.SaveChangesAsync();

            if (student.guardians.Count() > 0)
            {
                foreach (var item in student.guardians) { item.studentId = student.students.id.ToString(); }
                await _applicationDbContext.Guardians.AddRangeAsync(student.guardians);
                await _applicationDbContext.SaveChangesAsync();
            }

            student.address.studentId = student.students.id;
            _applicationDbContext.StudentAddresses.Add(student.address);
            await _applicationDbContext.SaveChangesAsync();
                var fatherDetail = student.guardians.Find(x => x.relationship == "Father");
                if (fatherDetail != null)
                {
                    createUserAccount(fatherDetail);
                }
            return true;
        }

        public async Task<List<Students>> list()
        {
            var res = await _applicationDbContext.Students.ToListAsync();
            return res;
        }

        public async Task<StudentGuardianRequest> StudentById(int id)
        {
            var studentRecord = _applicationDbContext.Students
                            .Where(x => x.id.Equals(id)).SingleOrDefault();
            var guardianRecords = await _applicationDbContext.Guardians
                            .Where(x => x.studentId.Equals(id.ToString())).ToListAsync();
            var studentAddress = _applicationDbContext.StudentAddresses
                                    .Where(x => x.studentId.Equals(id)).SingleOrDefault();
            if (studentRecord is Students)
            {
                studentRecord.photo = this.GetImage(Convert.ToBase64String(studentRecord.photo));
            }
            else
            {
                throw new EntityInvalidException("Invalid", "Record not found");
            }
            StudentGuardianRequest sgr = new StudentGuardianRequest()
            {
                students = studentRecord,
                guardians = guardianRecords,
                address = studentAddress
            };
            return sgr;
        }

        public async Task<bool> update(StudentGuardianRequest student)
        {
            //var stdRecord = _applicationDbContext.Students.FirstOrDefault(std => std.id == student.students.id);
            _applicationDbContext.Students.Update(student.students);
            await _applicationDbContext.SaveChangesAsync();

            if (student.guardians.Count() > 0)
            {
                foreach (var item in student.guardians) { 
                    _applicationDbContext.Guardians.Update(item);
                }
                await _applicationDbContext.SaveChangesAsync();
            }

            _applicationDbContext.StudentAddresses.Update(student.address);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public byte[] GetImage(string sBase64String)
        {
            byte[] bytes = null;
            if (!string.IsNullOrEmpty(sBase64String))
            {
                bytes = Convert.FromBase64String(sBase64String);
            }
            return bytes;
        }

        public List<StudentGuardianRequest> StudentsByClassName(string className)
        {
            var students = _applicationDbContext.Students.Where(x => x.className == className).ToList();
            var result = new List<StudentGuardianRequest>();
            foreach (var item in students)
            {
                var fatherDetail = _applicationDbContext.Guardians.SingleOrDefault(x => x.studentId == item.id.ToString() && x.relationship == "Father");
                var motherDetail = _applicationDbContext.Guardians.SingleOrDefault(x => x.studentId == item.id.ToString() && x.relationship == "Mother");

                result.Add(new StudentGuardianRequest()
                {
                    students = item,
                    guardians = new List<Guardian>() { fatherDetail, motherDetail },
                    address = null
                });
            }
            return result;
        }

        public List<StudentGuardianRequest> GetStudentsByRole(string role, string email)
        {
            List<Students> students = new List<Students>();

            var result = new List<StudentGuardianRequest>();
            if (role == StaticUserRoles.ADMIN ||
                role == StaticUserRoles.OWNER ||
                role == StaticUserRoles.TEACHER)
            {
                students = _applicationDbContext.Students.ToList();
            }
            else if (role == StaticUserRoles.PARENT && !string.IsNullOrEmpty(email))
            {
                var studentIds = _applicationDbContext.Guardians
                    .Where(x => x.email == email 
                    && !string.IsNullOrEmpty(x.studentId)
                    && !string.IsNullOrEmpty(x.email))
                    .Select(x => int.Parse(x.studentId))
                    .Distinct()
                    .ToList();

                students = _applicationDbContext.Students
                    .Where(x => studentIds.Contains(x.id))
                    .ToList();
            }
                foreach (var item in students)
                {
                    var fatherDetail = _applicationDbContext.Guardians.SingleOrDefault(x => x.studentId == item.id.ToString() && x.relationship == "Father");
                    var motherDetail = _applicationDbContext.Guardians.SingleOrDefault(x => x.studentId == item.id.ToString() && x.relationship == "Mother");
                    var studentAddress = _applicationDbContext.StudentAddresses
                                        .Where(x => x.studentId.Equals(item.id)).SingleOrDefault();
                result.Add(new StudentGuardianRequest()
                    {
                        students = item,
                        guardians = new List<Guardian>() { fatherDetail, motherDetail },
                        address = studentAddress
                    });
                }
            return result;
        }

        async void createUserAccount(Guardian fatherDetail)
        {
            var registerDTO = new RegisterDto();
            registerDTO.FirstName = fatherDetail.FirstName;
            registerDTO.LastName = fatherDetail.LastName;
            registerDTO.Email = fatherDetail.email;
            registerDTO.UserName = fatherDetail.FirstName + "" + fatherDetail.LastName.Substring(0, 3);
            registerDTO.Password = "Password@123";
            registerDTO.Role = StaticUserRoles.PARENT;

            await _authService.RegisterAsync(registerDTO);

            StringBuilder html = new StringBuilder();
            html.Append($"<h4>Hello {fatherDetail.FirstName}</h4><br/>");
            html.Append($"<p>Below are the credetials to login Skool UI App<p>");
            html.Append($"<p>UserName: {registerDTO.UserName}</p><br/><p>Password: Password@123</p>");
            MailRequest request = new MailRequest();
            request.ToEmail = fatherDetail.email;
            request.Subject = "Reset your Skool UI Password";
            request.Body = html.ToString();
            await _sendMail.SendEmailAsync(request);
        }
    }
}
