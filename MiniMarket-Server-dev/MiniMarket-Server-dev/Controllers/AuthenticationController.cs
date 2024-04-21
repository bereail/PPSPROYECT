using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_Server_dev.Application.DTOs.Requests;
using MiniMarket_Server_dev.Application.Services.Interfaces;

namespace MiniMarket_Server_dev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomAuthenticationService authenticationService;

        public AuthenticationController(IConfiguration configuration,ICustomAuthenticationService authenticationService)
        {
            _configuration = configuration;
            this.authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate(LoginRequestDTO loginRequest)
        {
            string? token = await authenticationService.Authenticate(loginRequest);
            //This should be removed when we implement proper exception handling.
            if (token == null)
            {
                return BadRequest("Failed to authenticate");
            }
            return Ok(token);
        }
    }
}
