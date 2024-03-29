﻿using School.API.Core.Entities;
using School.API.Core.Models.StudentRequestModel;

namespace School.API.Core.Interfaces
{
    public interface IStudent
    {
        Task<StudentGuardianRequest> StudentById(int id);
        Task<List<Students>> list();
        Task<bool> create(StudentGuardianRequest student);
        Task<bool> update(Students student);
    }
}
