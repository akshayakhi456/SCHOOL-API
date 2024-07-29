using School.API.Core.Entities;

namespace School.API.Core.Interfaces
{
    public interface ITitleHeader
    {
        string SaveTitle(TitleHeader titleHeader);
        List<TitleHeader> getTitleList();
        TitleHeader getTitle(string title);
    }
}
