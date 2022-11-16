using PMS.DAL.IRepos;
using PMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PMS.Domain.Models.Queries;

namespace PMS.DAL.Repos
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        public UserRepository(PMSDbContext context) : base(context)
        {

        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(i => i.Roles).Include(i => i.UserClaims)
                .FirstOrDefaultAsync(m => m.Id == id);
        }


        public async Task<User> GetLoginUserAsync(string username, string password)
        {
            //check email/username/phone
            User user = null;

            user = _context.Users
                .Include(i => i.Roles).Include(i => i.UserClaims)
                .FirstOrDefault(m => m.UserName == username && m.Password == password);

            if (user == null)
            {
                user = _context.Users
                    .Include(i => i.Roles).Include(i => i.UserClaims)
                    .FirstOrDefault(m => m.Email == username && m.Password == password);
            }
            if (user == null)
            {
                user = _context.Users
                    .Include(i => i.Roles).Include(i => i.UserClaims)
                    .FirstOrDefault(m => m.PhoneNumber == username && m.Password == password);
            }

            return user;
        }

        

        public async Task<QueryResult<User>> ListAsync(UsersQuery query)
        {
            IQueryable<User> queryable = _context.Users.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                queryable = queryable.Where(x => x.FirstName.Contains(query.Name) || x.LastName.Contains(query.Name));
            }
            if (!string.IsNullOrWhiteSpace(query.UserName))
            {
                queryable = queryable.Where(x => x.UserName.Contains(query.UserName));
            }
            if (!string.IsNullOrWhiteSpace(query.PhoneNumber))
            {
                queryable = queryable.Where(x => x.PhoneNumber.Contains(query.PhoneNumber));
            }
            if (!string.IsNullOrWhiteSpace(query.Email))
            {
                queryable = queryable.Where(x => x.Email.Contains(query.Email));
            }

            int totalItems = await queryable.CountAsync();

            List<User> users = await queryable.Skip((query.Page - 1) * query.ItemsPerPage)
                                                    .Take(query.ItemsPerPage)
                                                    .ToListAsync();

            return new QueryResult<User>
            {
                Items = users,
                TotalItems = totalItems,
            };
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
