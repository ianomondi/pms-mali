 using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Domain.IServices;
using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using static PMS.Domain.Resources.Request.UsersRequest;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, ILogger<UsersController> logger, IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<ActionResult> List([FromQuery]UserListRequest request)
        {
            var query = _mapper.Map<UserListRequest, UsersQuery>(request);

            var response = await _userService.ListAsync(query);

            return Ok(response);
        }

        [HttpPost]
        [Route("add-user")]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<ActionResult> Create(CreateUser request)
        {

            var user = _mapper.Map<User>(request);
            var response = await _userService.AddAsync(user);

            return Ok(response);
        }

        [HttpPut]
        [Route("update-user")]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<ActionResult> Update(UpdateUser request)
        {

            var user = _mapper.Map<User>(request);
            var response = await _userService.UpdateAsync(user.Id, user);

            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-user")]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<ActionResult> Delete(DeleteUser request)
        {

            var response = await _userService.RemoveAsync(request.Id);

            return Ok(response);
        }
    }
}
