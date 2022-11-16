using Microsoft.AspNetCore.Authorization;
using PMS.DAL;
using PMS.Domain.Models;
using PMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace PMS.API.Authorization
{
    public class RoleHasPermissionRequirement : IAuthorizationRequirement
    {
        public RoleHasPermissionRequirement(string role, string permission)
        {
            Role = role;
            Permission = permission;
        }

        public string Role { get; }
        public string Permission { get; }
    }

    public class RoleHasPermissionHandler : AuthorizationHandler<RoleHasPermissionRequirement>
    {
        PMSDbContext _context;
        readonly IHttpContextAccessor _httpAccessor;
        readonly IUserAuthService _userAuthService;

        public RoleHasPermissionHandler(IHttpContextAccessor accessor, PMSDbContext c, IUserAuthService userAuthService)
        {

            _httpAccessor = accessor;
            _userAuthService = userAuthService;
            _context = c; // new PMSDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<PMSDbContext>(), _httpAccessor);
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleHasPermissionRequirement requirement)
        {
            if (!context.User.IsInRole(requirement.Role))
            {
                return Task.CompletedTask;
            }

            var dbUser = _userAuthService.GetAuthenticatedUser();


            if (dbUser == null || dbUser.Roles == null)
            {
                return Task.CompletedTask;
            }

            var role = dbUser.Roles
                .FirstOrDefault(m => m.Name == requirement.Role);

            if (role == null)
            {
                return Task.CompletedTask;
            }

            //get role
            var dbRole = _context.Roles
                .Include(m => m.RolePermissions)
                .FirstOrDefault(m => m.Id == role.Id);

            if(dbRole == null || dbRole.RolePermissions == null)
            {
                return Task.CompletedTask;
            }

            //check if has permission
            if(role.RolePermissions.Any(m => m.Permission == requirement.Permission))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
