using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper mapper;

        public CustomerController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewCustomerAsync([FromBody] CreateUserDto createCustomer)
        {
            var customerToCreate = mapper.Map<Customer>(createCustomer);

            var createdCustomer = await _userService.CreateUser(customerToCreate);
            if (createdCustomer == null)
            {
                return Conflict("Registration Failed: Email currently in Use!");
            }
            return Ok(createdCustomer);
        }
    }
}
