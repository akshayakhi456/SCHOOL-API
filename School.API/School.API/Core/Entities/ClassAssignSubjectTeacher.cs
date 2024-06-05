﻿namespace School.API.Core.Entities
{
    public class ClassAssignSubjectTeacher
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SubjectId { get; set; }
        public int SubjectTeacherId { get; set; }
        public bool IsClassTeacher { get; set; }
        public int academicYearId { get; set; }
        public Classes Classes { get; set; }
        public Subject Subject { get; set; }
        public Section Section { get; set; }
        public TeacherDetails TeacherDetails { get; set; }
    }
}
