using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Domain.IServices;
using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using static PMS.Domain.Resources.Request.TripRequest;
using static PMS.Domain.Resources.Request.UsersRequest;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public TripsController(ITripService tripService, ILogger<UsersController> logger, IMapper mapper)
        {
            _tripService = tripService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<ActionResult> List([FromQuery] TripListRequest request)
        {
            var query = _mapper.Map<TripListRequest, TripsQuery>(request);

            var response = await _tripService.ListAsync(query);

            return Ok(response);
        }

        [HttpPost]
        [Route("add-trip")]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<ActionResult> Create(CreateTrip request)
        {

            var trip = _mapper.Map<Trip>(request);
            var response = await _tripService.AddAsync(trip);

            return Ok(response);
        }

        [HttpPut]
        [Route("update-trip")]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<ActionResult> Update(UpdateTrip request)
        {

            var trip = _mapper.Map<Trip>(request);
            var response = await _tripService.UpdateAsync(trip.Id, trip);

            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-trip")]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<ActionResult> Delete(DeleteTrip request)
        {

            var response = await _tripService.RemoveAsync(request.Id);

            return Ok(response);
        }
    }
}
