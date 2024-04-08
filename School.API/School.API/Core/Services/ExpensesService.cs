using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.EnquiryRequestResponseModel;
using System.Linq;

namespace School.API.Core.Services
{
    public class ExpensesService : IExpenses
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ExpensesService(ApplicationDbContext applicationDbContext) {
            _applicationDbContext = applicationDbContext;
        }
        public List<Expenses> list()
        {
            var res = _applicationDbContext.Expenses.ToList();
            return res;
        }

        public bool create(Expenses expenses)
        {
            _applicationDbContext.Expenses.Add(expenses);
            _applicationDbContext.SaveChanges();
            return true;
        }

        public List<ExpenseGraphResponse> expensesGraph()
        {
            var record = _applicationDbContext.Expenses.GroupBy(d => d.doe.Month).ToList();
            return record.Select(c => new ExpenseGraphResponse
            {
                count = (int)c.Sum(x => x.amount),
                month = c.FirstOrDefault().doe.Month
            }).OrderBy(x=>x.month).ToList();
        }
    }
}
