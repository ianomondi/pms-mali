using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Domain.IServices;
using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using static PMS.Domain.Resources.Request.ExpenseRequest;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseervice;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public ExpensesController(IExpenseService
            expenseervice, ILogger<UsersController> logger, IMapper mapper)
        {
            _expenseervice = expenseervice;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<ActionResult> List([FromQuery] ExpenseListRequest request)
        {
            var query = _mapper.Map<ExpenseListRequest, ExpensesQuery>(request);

            var response = await _expenseervice.ListAsync(query);

            return Ok(response);
        }

        [HttpPost]
        [Route("add-trip")]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<ActionResult> Create(CreateExpense request)
        {

            var expense = _mapper.Map<Expense>(request);
            var response = await _expenseervice.AddAsync(expense);

            return Ok(response);
        }

        //[HttpPut]
        //[Route("update-trip")]
        //[Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        //public async Task<ActionResult> Update(UpdateExpense request)
        //{

        //    var trip = _mapper.Map<Trip>(request);
        //    var response = await _expenseervice.UpdateAsync(trip.Id, trip);

        //    return Ok(response);
        //}

        //[HttpDelete]
        //[Route("delete-trip")]
        //[Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        //public async Task<ActionResult> Delete(DeleteTrip request)
        //{

        //    var response = await _tripService.RemoveAsync(request.Id);

        //    return Ok(response);
        //}
    }
}
