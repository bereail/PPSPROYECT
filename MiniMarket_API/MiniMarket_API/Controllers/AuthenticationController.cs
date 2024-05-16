using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;

namespace MiniMarket_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomAuthenticationService authenticationService;

        public AuthenticationController(IConfiguration configuration, ICustomAuthenticationService authenticationService)
        {
            _configuration = configuration;
            this.authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(LoginRequestDTO loginRequest)
        {
            string? token = await authenticationService.Authenticate(loginRequest);
            
            return Ok(token);
        }
    }
}
