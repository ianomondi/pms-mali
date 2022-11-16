using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Domain.IServices;
using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using PMS.Domain.Resources.Request;
using static PMS.Domain.Resources.Request.VehicleRequest;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly ILogger<VehiclesController> _logger;
        private readonly IMapper _mapper;
        private readonly IVehicleService _vehicleService;

        public VehiclesController(ILogger<VehiclesController> logger,IMapper mapper, IVehicleService vehicleService)
        {
            _logger = logger;
            _mapper = mapper;
            _vehicleService = vehicleService;
        }
        [HttpGet]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<IActionResult> List([FromQuery] VehicleListRequest request)
        {
            var query = _mapper.Map<VehicleListRequest, VehiclesQuery>(request);
            var response = await _vehicleService.ListAsync(query);
            return Ok(response);
        }

        [HttpPost]
        [Route("add-vehicle")]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<IActionResult> Create(CreateVehicle request)
        {
            var vehicle = _mapper.Map<Vehicle>(request);
            var response= await _vehicleService.AddAsync(vehicle);
            return Ok(response);
        }
        [HttpPut]
        [Route("update-vehicle")]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<ActionResult> Update(UpdateVehicle request)
        {

            var vehicle = _mapper.Map<Vehicle>(request);
            var response = await _vehicleService.UpdateAsync(vehicle.Id, vehicle);

            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-vehicle")]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<ActionResult> Delete(DeleteVehicle request)
        {

            var response = await _vehicleService.RemoveAsync(request.Id);

            return Ok(response);
        }
    }
}
