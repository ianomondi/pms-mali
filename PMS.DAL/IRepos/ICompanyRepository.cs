using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.IRepos
{
    public interface ICompanyRepository
    {
        Task<Company> GetByIdAsync(Guid id);
        Task<QueryResult<Company>> ListAsync(CompaniesQuery query);
        Task AddAsync(Company company);
        void Remove(Company company);
        void Update(Company company);
    }
}
