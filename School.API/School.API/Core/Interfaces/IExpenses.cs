using School.API.Core.Entities;

namespace School.API.Core.Interfaces
{
    public interface IExpenses
    {
        List<Expenses> list();
        bool create(Expenses expenses);

    }
}
