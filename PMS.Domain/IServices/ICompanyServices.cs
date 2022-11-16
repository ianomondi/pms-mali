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
    public interface ICompanyServices
    {
        Task<Company> GetByIdAsync(Guid id);
        Task<CompanyReponse> AddAsync(Company company);
        Task<CompanyReponse> RemoveAsync(Guid id);
        Task<CompanyReponse> UpdateAsync(Guid id, Company company);
        //Task<IEnumerable<Company>> ListAsync();
        Task<QueryResult<Company>> ListAsync(CompaniesQuery query);
    }
}
