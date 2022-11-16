using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repos
{
    public abstract class BaseRepository
    {
        protected readonly PMSDbContext _context;

        public BaseRepository(PMSDbContext context)
        {
            _context = context;
        }
    }
}
