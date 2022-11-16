using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Domain.IServices;
using PMS.Domain.Models;
using PMS.Domain.Models.Queries;
using PMS.Domain.Resources.Request.Companies;
using PMS.Services.Contracts;
using System.Security.Claims;

namespace PMS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ILogger<CompaniesController> _logger;
        private readonly ICompanyServices _companyServices;
        private readonly IUserAuthService _userAuthService;
        private readonly IMapper _mapper;

        public CompaniesController(ILogger<CompaniesController> logger, ICompanyServices companyServices, IUserAuthService userAuthService, IMapper mapper)
        {
            _logger = logger;
            _companyServices = companyServices;
            _userAuthService = userAuthService;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("add-company")]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<IActionResult> Create(CreateRequest request)
        {
            var company = _mapper.Map<Company>(request);
            var response = await _companyServices.AddAsync(company);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<ActionResult> Index([FromQuery] CompanyListRequest query)
        {
            var currentUser = _userAuthService.GetAuthenticatedUser();

            CompaniesQuery q = _mapper.Map<CompanyListRequest, CompaniesQuery>(query);

            var companies = await _companyServices.ListAsync(q);

            return Ok(companies);
        }

        [HttpPost]
        [Route("edit-company")]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<IActionResult> Update(EditRequest request)
        {
            var company = _mapper.Map<Company>(request);
            var response = await _companyServices.UpdateAsync(company.Id, company);
            return Ok(response);
        }

        [HttpPost]
        [Route("delete-company")]
        [Authorize(Policy = PolicyMapper.CAN_ROLE_ADD_USER)]
        public async Task<IActionResult> Delete(DeleteRequest request)
        {
            var response = await _companyServices.RemoveAsync(request.Id);

            return Ok(response);
        }
    }
}
