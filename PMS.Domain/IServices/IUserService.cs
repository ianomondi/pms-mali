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
    public interface IUserService
    {
        Task<UserResponse> AddAsync(User user);
        Task<QueryResult<User>> ListAsync(UsersQuery query);
        Task<UserResponse> RemoveAsync(Guid id);
        Task<UserResponse> UpdateAsync(Guid id, User user);
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetLoginUserAsync(string username, string password);
    }
}
