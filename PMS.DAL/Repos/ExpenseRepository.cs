using PMS.DAL.IRepos;
using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repos
{
    public class ExpenseRepository : BaseRepository, IExpenseRepository
    {
        public ExpenseRepository(PMSDbContext context) : base(context)
        {

        }
        public Task AddAsync(Expense expense)
        {
            throw new NotImplementedException();
        }

        public Task<Expense> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<QueryResult<Trip>> ListAsync(ExpensesQuery query)
        {
            throw new NotImplementedException();
        }

        public void Remove(Expense expense)
        {
            throw new NotImplementedException();
        }

        public void Update(Expense expense)
        {
            throw new NotImplementedException();
        }
    }
}
