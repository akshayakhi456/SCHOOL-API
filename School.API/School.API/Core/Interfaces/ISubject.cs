﻿using School.API.Core.Entities;

namespace School.API.Core.Interfaces
{
    public interface ISubject
    {
        string addMarks(List<StudentMarks> studentMarks);

        List<StudentMarks> getMarksByStudentId(int studentId, int acedemicYearId);
    }
}