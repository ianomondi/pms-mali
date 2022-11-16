using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using PMS.Domain.Models;
using System.Linq;

/*using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;*/

namespace PMS.DAL
{
    public partial class PMSDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PMSDbContext(DbContextOptions<PMSDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;

            //var dependencies = DependencyContext.Default.RuntimeLibraries;
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}