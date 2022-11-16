using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Domain.IServices;
using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using PMS.Domain.Resources.Request.Roles;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IRoleService _roleServices;
        private readonly IMapper _mapper;

        public RolesController(ILogger<RolesController> logger, IRoleService roleServices, IMapper mapper)
        {
            _logger = logger;
            _roleServices = roleServices;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("add-role")]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<ActionResult> Create(RolesRequest.CreateRole request)
        {

            var role = _mapper.Map<Role>(request);
            var response = await _roleServices.AddAsync(role);

            return Ok(response);
        }
        
        [HttpPut]
        [Route("update-role")]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<ActionResult> Update(RolesRequest.UpdateRole request)
        {

            var role = _mapper.Map<Role>(request);
            var response = await _roleServices.UpdateAsync(role.Id,role);

            return Ok(response);
        }
        
        [HttpGet]
        [Route("list-roles")]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<ActionResult> List([FromQuery] RolesRequest.RoleListRequest request)
        {

            var qr = _mapper.Map<RolesQuery>(request);

            var response = await _roleServices.ListAsync(qr);

            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-role")]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<ActionResult> Delete(RolesRequest.DeleteRole request)
        {

            var response = await _roleServices.RemoveAsync(request.Id);

            return Ok(response);
        }
    }
}
