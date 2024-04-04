using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using static System.Collections.Specialized.BitVector32;
using Section = School.API.Core.Entities.Section;

namespace School.API.Core.Services
{
    public class SettingsService : ISettings
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public SettingsService(ApplicationDbContext applicationDbContext) {
            _applicationDbContext = applicationDbContext;
        }
         public string createClass(Classes classes)
        {
            if (classes is not null )
            {
                var isExistClass = _applicationDbContext.classes.Any(c => c.className == classes.className);
                if (isExistClass)
                {
                    return "Already Class Exist";
                }
               _applicationDbContext.classes.Add(classes);
               _applicationDbContext.SaveChanges();
                return "Created Successfully";
            }
            return "Something went wrong";
        }

        public List<Classes> getClasses()
        {
            return _applicationDbContext.classes.ToList();
        }

        public bool updateClass(Classes classes)
        {
            _applicationDbContext.classes.Update(classes);
            _applicationDbContext.SaveChanges();
            return true;
        }

        public Classes getClassById(int id)
        {
            return _applicationDbContext.classes.Find(id);
        }

        public bool deleteClass(int id)
        {
            var rec = _applicationDbContext.classes.Where(x => x.id == id).FirstOrDefault();
            _applicationDbContext.classes.Remove(rec);
            _applicationDbContext.SaveChanges();
            return true;
        }

        public string createSection(Section section)
        {
            if (section is not null)
            {
                var isExistSection = _applicationDbContext.section.Any(x => x.section == section.section && x.className == section.className);
                if (isExistSection) {
                    return "Already Section Exist";
                }
                _applicationDbContext.section.Add(section);
                _applicationDbContext.SaveChanges();
                return "Created Successully";
            }
            return "Something went wrong";
        }

        public List<Section> getSections()
        {
            return _applicationDbContext.section.ToList();
        }

        public List<Section> getSectionsByClassName(string className)
        {
            return _applicationDbContext.section.Where(x=>x.className == className).ToList();
        }

        public bool updateSection(Section section)
        {
            _applicationDbContext.section.Update(section);
            _applicationDbContext.SaveChanges();
            return true;
        }

        public bool deleteSection(int id)
        {
            var rec = _applicationDbContext.section.Where(x => x.id == id).FirstOrDefault();
            _applicationDbContext.section.Remove(rec);
            _applicationDbContext.SaveChanges();
            return true;
        }

        public string createEnquiryQuestion(EnquiryQuestions enquiryQuestions)
        {
            _applicationDbContext.enquiryQuestions.Add(enquiryQuestions);
            _applicationDbContext.SaveChanges();
            return "Created Successully";
        }

        public string updateEnquiryQuestion(EnquiryQuestions enquiryQuestions)
        {
            _applicationDbContext.enquiryQuestions.Update(enquiryQuestions);
            _applicationDbContext.SaveChanges();
            return "Updated Successfully";
        }

        public List<EnquiryQuestions> getAllEnquiryQuestion()
        {
            var list = _applicationDbContext.enquiryQuestions.ToList();
            return list;
        }
    }
}
