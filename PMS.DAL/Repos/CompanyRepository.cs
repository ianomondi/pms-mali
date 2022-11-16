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
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {

        public CompanyRepository(PMSDbContext context) : base(context)
        {

        }

        public async Task AddAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
        }

        public async Task<Company> GetByIdAsync(Guid id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task<QueryResult<Company>> ListAsync(CompaniesQuery query)
        {
           
            IQueryable<Company> queryable = _context.Companies.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                queryable = queryable.Where(x => x.Name.Contains(query.Name));
            }
            if (!string.IsNullOrWhiteSpace(query.PhoneNumber))
            {
                queryable = queryable.Where(x => x.PhoneNumber.Contains(query.PhoneNumber));
            }
            if (!string.IsNullOrWhiteSpace(query.Email))
            {
                queryable = queryable.Where(x => x.Email.Contains(query.Email));
            }
            if (!string.IsNullOrWhiteSpace(query.Status))
            {
                queryable = queryable.Where(x => x.Status.Contains(query.Status));
            }

            // Here I count all items present in the database for the given query, to return as part of the pagination data.
            int totalItems = await queryable.CountAsync();

            // Here I apply a simple calculation to skip a given number of items, according to the current page and amount of items per page,
            // and them I return only the amount of desired items. The methods "Skip" and "Take" do the trick here.
            List<Company> companies = await queryable.Skip((query.Page - 1) * query.ItemsPerPage)
                                                    .Take(query.ItemsPerPage)
                                                    .ToListAsync();

            // Finally I return a query result, containing all items and the amount of items in the database (necessary for client-side calculations ).
            return new QueryResult<Company>
            {
                Items = companies,
                TotalItems = totalItems,
            };
        }

        /*public async Task<<QueryResult<Company>> ListAsync(CompaniesQuery query)
        {
            throw new NotImplementedException();
            //return await _context.Companies.ToListAsync();
        }*/

        public void Remove(Company company)
        {
           _context.Companies.Remove(company);
        }

        public void Update(Company company)
        {
            _context.Companies.Update(company);
        }
    }
}
