using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PMS.DAL;
using PMS.Domain.IServices;
using PMS.Domain.Models;
using PMS.Domain.Resources.Request;
using PMS.Domain.Resources.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        public IUserService _userService;
        private readonly ILogger<CompaniesController> _logger;

        public TokenController(IConfiguration config, ILogger<CompaniesController> logger, IUserService context)
        {
            _configuration = config;
            _logger = logger;
            _userService = context;
        }

        

        [HttpPost]
        public async Task<LoginResponse> Post(LoginRequest _userData)
        {


            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Email, _userData.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claimsList = new List<Claim> {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(CustomClaimTypes.UserId, user.Id.ToString()),
                        new Claim(CustomClaimTypes.UserDisplayName, user.DisplayName),
                        new Claim(CustomClaimTypes.UserName, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                    };

                    var userRoles = user.Roles;

                    if(userRoles != null && userRoles.Any())
                    {
                        foreach(var role in userRoles)
                        {
                            claimsList.Add(new Claim(
                                ClaimTypes.Role, role.Name
                            ));
                        }    
                    }
                    
                    var userClaims = user.UserClaims;

                    if(userClaims != null && userClaims.Any())
                    {
                        foreach(var claim in userClaims)
                        {
                            claimsList.Add(new Claim(
                                claim.ClaimType, claim.ClaimValue
                            ));
                        }    
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claimsList,
                        
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: signIn);

                    //DateTime start = token.ValidFrom;
                    DateTime expiry = token.ValidTo;

                    var currTime = DateTime.Now;

                    var tokenGenerated = new TokenGenerated
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiresAt = currTime.AddMinutes(30),
                        //issuedAt = DateTime.Now,
                    };

                    return new LoginResponse(tokenGenerated,"Login successfully");
                }
                else
                {
                    return new LoginResponse("Login failed");
                }
            }
            else
            {
                return new LoginResponse("Bad request");
            }
        }

        private async Task<User> GetUser(string email, string password)
        {
            return await _userService.GetLoginUserAsync(email,password);
        }
    }
}
