using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;

namespace School.API.Core.Services
{
    public class TitleHeaderService : ITitleHeader
    {
        private ApplicationDbContext _applicationDbContext;
        public TitleHeaderService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        List<TitleHeader> ITitleHeader.getTitleList()
        {
            return _applicationDbContext.TitleHeaders.ToList();
        }

        TitleHeader ITitleHeader.getTitle(string title)
        {
            return _applicationDbContext.TitleHeaders.FirstOrDefault(x => x.Title == title);
        }

        string ITitleHeader.SaveTitle(TitleHeader titleHeader)
        {
            var item = _applicationDbContext.TitleHeaders.FirstOrDefault(x => x.Title == titleHeader.Title);
            if (item != null)
            {
                item.Photo = titleHeader.Photo;
                item.Description = titleHeader.Description;
                _applicationDbContext.SaveChanges();
            }
            else
            {
                _applicationDbContext.TitleHeaders.Add(titleHeader);
                _applicationDbContext.SaveChanges();
            }
            return "Saved Successfully";
        }
    }
}
