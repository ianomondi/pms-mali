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
    public interface IExpenseService
    {
        Task<ExpenseResponse> AddAsync(Expense expense);
        Task<QueryResult<Expense>> ListAsync(ExpensesQuery query);
        Task<ExpenseResponse> RemoveAsync(Guid id);
        Task<ExpenseResponse> UpdateAsync(Guid id, Expense expense);
        Task<Expense> GetByIdAsync(Guid id);
    }
}
