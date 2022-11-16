using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using PMS.Domain.Resources.Request.Roles;
using PMS.Domain.Resources.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.IServices
{
    public interface IRoleService
    {
        Task<Role> GetByIdAsync(Guid id);
        Task<BaseResponse<Role>> AddAsync(Role role);
        Task<BaseResponse<Role>> RemoveAsync(Guid id);
        Task<BaseResponse<Role>> UpdateAsync(Guid id, Role role);
        Task<QueryResult<Role>> ListAsync(RolesQuery query);
    }
}
