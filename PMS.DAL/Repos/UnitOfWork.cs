using PMS.DAL.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PMSDbContext _context;

        public UnitOfWork(PMSDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
