﻿namespace School.API.Core.Entities
{
    public class StudentClassSection
    {
        public int Id { get; set; }
        public int Studentsid { get; set; }
        public int RollNo { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int AcademicYearId { get; set; }
        public virtual Students Students { get; set; }
        public virtual Section Section { get; set; }
    }
}
