using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.IRepos
{
    public interface IExpenseRepository
    {
        Task<Expense> GetByIdAsync(Guid id);
        Task AddAsync(Expense expense);
        Task<QueryResult<Trip>> ListAsync(ExpensesQuery query);
        void Update(Expense expense);
        void Remove(Expense expense);
    }
}
