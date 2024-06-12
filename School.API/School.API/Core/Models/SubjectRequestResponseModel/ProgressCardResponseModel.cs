namespace School.API.Core.Models.SubjectRequestResponseModel
{
    public class ProgressCardResponseModel
    {
        public StudentInfo StudentInfo { get; set; }
        public List<SubjectInfo> SubjectInfos { get; set; }
    }

    public class StudentInfo
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public int AdmNo { get; set; }
        public int RollNo { get; set; }
        public string ClassName { get; set; }
        public string Section { get; set; }
        public string DateOfBirth { get; set; }
    }

    public class SubjectInfo
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public int Marks { get; set; }
        public string MaxMarks { get; set; }
        public string Remarks { get; set; }
    }
}
