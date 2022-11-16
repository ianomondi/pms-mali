using Microsoft.EntityFrameworkCore;
using PMS.DAL.IRepos;
using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using PMS.Domain.Resources.Request.Roles;
using PMS.Domain.Resources.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repos
{
    public class RolesRepo : BaseRepository, IRolesRepo
    {
        public RolesRepo(PMSDbContext context) : base(context)
        {

        }

        public async Task AddAsync(Role role)
        {
            var existingRole = await _context.Roles.FirstOrDefaultAsync(m => m.Name == role.Name);

            if (existingRole != null) throw new Exception("Role already exists");

            await _context.Roles.AddAsync(role);
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<QueryResult<Role>> ListAsync(RolesQuery query)
        {

            IQueryable<Role> queryable = _context.Roles.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                queryable = queryable.Where(x => x.Name.Contains(query.Name));
            }
            

            // Here I count all items present in the database for the given query, to return as part of the pagination data.
            int totalItems = await queryable.CountAsync();

            // Here I apply a simple calculation to skip a given number of items, according to the current page and amount of items per page,
            // and them I return only the amount of desired items. The methods "Skip" and "Take" do the trick here.
            
            List<Role> roles = await queryable.Skip((query.Page - 1) * query.ItemsPerPage)
                                                    .Take(query.ItemsPerPage)
                                                    .ToListAsync();

            // Finally I return a query result, containing all items and the amount of items in the database (necessary for client-side calculations ).
            return new QueryResult<Role>
            {
                Items = roles,
                TotalItems = totalItems,
            };
        }


        public void Remove(Role role)
        {
            _context.Roles.Remove(role);
        }

        public void Update(Role role)
        {
            var existingRole = _context.Roles.FirstOrDefault(m => m.Name == role.Name && m.Id != role.Id);
            if (existingRole != null) throw new Exception("Role already exists");
            _context.Roles.Update(role);
        }

        
    }
}
