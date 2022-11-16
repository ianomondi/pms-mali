using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using PMS.Domain;
using PMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL
{
    public partial class PMSDbContext
    {
        public void SetGlobalQuery<T>(ModelBuilder builder) where T : BaseEntity
        {
            builder.Entity<T>().HasKey(e => e.Id);
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        static readonly MethodInfo SetGlobalQueryMethod = typeof(PMSDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");
        private static IList<Type> _entityTypeCache;

        private static IList<Type> GetEntityTypes()
        {
            if (_entityTypeCache != null)
            {
                return _entityTypeCache.ToList();
            }

            _entityTypeCache = (from a in GetReferencingAssemblies()
                                from t in a.DefinedTypes
                                where t.BaseType == typeof(BaseEntity)
                                select t.AsType()).ToList();

            return _entityTypeCache;
        }

        private static IEnumerable<Assembly> GetReferencingAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            foreach (var library in dependencies)
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
                catch (FileNotFoundException)
                { }
            }
            return assemblies;
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity trackable)
                {
                    var now = DateTime.UtcNow;
                    var userId = GetLoggedInEmployeeId();
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.LastModified = now;
                            trackable.ModifiedById = Guid.Parse(userId);
                            break;

                        case EntityState.Added:
                            //trackable.Id = Guid.NewGuid();
                            trackable.DateCreated = now;
                            trackable.LastModified = now;
                            trackable.IsDeleted = false;
                            trackable.ModifiedById = Guid.Parse(userId);
                            trackable.CreatedById = Guid.Parse(userId);
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            trackable.ModifiedById = Guid.Parse(userId);
                            trackable.LastModified = now;
                            trackable.IsDeleted = true;
                            break;
                    }
                }
            }
        }

        private string GetLoggedInEmployeeId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                if (httpContext.User != null)
                {
                    var user = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                    if (user != null)
                    {
                        var claims = _httpContextAccessor.HttpContext.User.Identities.First().Claims.ToList();
                        var userId = claims.First(m => m.Type == CustomClaimTypes.UserId).Value;
                        if (string.IsNullOrWhiteSpace(userId)) return null;

                        return userId;  

                        /*var userIdStr = user.Value;
                        return userIdStr;*/
                    }
                }
            }
            return null;
        }

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var type in GetEntityTypes())
            {
                var method = SetGlobalQueryMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] { modelBuilder });
            }

            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.PhoneNumber).IsUnique();

            modelBuilder.Entity<User>()
                .HasMany<Role>(s => s.Roles)
                .WithMany(c => c.Users)
                .UsingEntity(j =>
                {
                    j.ToTable("UserRoles");
                });


            base.OnModelCreating(modelBuilder);

        }

    }
}
