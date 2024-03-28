﻿using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;

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
    }
}
