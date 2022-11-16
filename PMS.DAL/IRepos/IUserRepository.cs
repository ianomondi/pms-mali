using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.IRepos
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task<QueryResult<User>> ListAsync(UsersQuery query);
        void Update(User user);
        void Remove(User user);
        Task<User> GetLoginUserAsync(string username, string password);
    }
}
