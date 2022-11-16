using Microsoft.AspNetCore.Http;
using PMS.Domain.IServices;
using PMS.Domain.Models;
using PMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services.DomainServices
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public UserAuthService(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService; 
        }

        public User GetAuthenticatedUser()
        {
            var user = _httpContextAccessor.HttpContext.User;

            if(user == null 
                ||  _httpContextAccessor.HttpContext.User.Identity == null 
                || !_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return null;
            }

            var claims = _httpContextAccessor.HttpContext.User.Identities.First().Claims.ToList();

            var userId = claims.First(m => m.Type == CustomClaimTypes.UserId).Value;

            /*if(userId == null || Convert.ToInt32(userId) == 0)
            {
                return null;
            }*/

            if (string.IsNullOrWhiteSpace(userId)) return null;

            //return user by id
            //int uid = Convert.ToInt32(userId);
            var call = _userService.GetByIdAsync(Guid.Parse(userId));

            call.Wait();

            return call.Result;
        }
    }
}
