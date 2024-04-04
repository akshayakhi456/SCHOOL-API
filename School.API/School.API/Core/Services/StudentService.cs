using Microsoft.EntityFrameworkCore;
using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.StudentRequestModel;

namespace School.API.Core.Services
{
    public class StudentService : IStudent
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public StudentService(ApplicationDbContext applicationDbContext) {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<bool> create(StudentGuardianRequest student)
        {
            var res = _applicationDbContext.Students.Add(student.students);
            await _applicationDbContext.SaveChangesAsync();

            if (student.guardians.Count() > 0)
            {
                foreach (var item in student.guardians) { item.studentId = student.students.id.ToString(); }
                _applicationDbContext.Guardians.AddRange(student.guardians);
                await _applicationDbContext.SaveChangesAsync();
            }

            student.address.studentId = student.students.id;
            _applicationDbContext.StudentAddresses.Add(student.address);
            await _applicationDbContext.SaveChangesAsync();
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
            studentRecord.photo = this.GetImage(Convert.ToBase64String(studentRecord.photo));
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
    }
}
