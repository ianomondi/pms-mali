using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using PMS.Domain.Resources.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.IServices
{
    public class ExpenseService : IExpenseService
    {
        public Task<ExpenseResponse> AddAsync(Expense expense)
        {
            throw new NotImplementedException();
        }

        public Task<Expense> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<QueryResult<Expense>> ListAsync(ExpensesQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<ExpenseResponse> RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ExpenseResponse> UpdateAsync(Guid id, Expense expense)
        {
            throw new NotImplementedException();
        }
    }
}
