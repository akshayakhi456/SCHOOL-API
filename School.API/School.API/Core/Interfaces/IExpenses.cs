using School.API.Core.Entities;
using School.API.Core.Models.EnquiryRequestResponseModel;

namespace School.API.Core.Interfaces
{
    public interface IExpenses
    {
        List<Expenses> list();
        bool create(Expenses expenses);
        List<ExpenseGraphResponse> expensesGraph();

    }
}
