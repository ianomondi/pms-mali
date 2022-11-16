using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using PMS.Domain.Resources.Request.Roles;
using PMS.Domain.Resources.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.IRepos
{
    public interface IRolesRepo
    {
        Task<Role> GetByIdAsync(Guid id);
        Task<QueryResult<Role>> ListAsync(RolesQuery query);
        Task AddAsync(Role company);
        void Update(Role role);
        void Remove(Role role);
    }
}
