using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests.Credentials;
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
        public async Task<IActionResult> Authenticate([FromBody] LoginRequestDTO loginRequest)
        {
            string? token = await authenticationService.Authenticate(loginRequest);
            
            return Ok(token);
        }

        [HttpPost("recovery")]
        public async Task<IActionResult> RequestPasswordRecovery([FromBody] PasswordRecoveryRequestDto passwordRecoveryRequest)
        {
            string email = passwordRecoveryRequest.Email;

            await authenticationService.HandlePasswordRecoveryRequest(email);

            //The result should not inform the user if the email was successfully sent.
            return NoContent();
        }

        [HttpPut("recovery")]
        [Authorize(AuthenticationSchemes = "PasswordRecovery")]
        public async Task<IActionResult> SetNewUserPassword([FromBody] NewPasswordRequestDto newPasswordRequest)
        {
            string newPassword = newPasswordRequest.Password;

            //This will be replaced with the actual password update logic down the line.
            string yourPassword = $"Your new password is {newPassword}";

            //The return will be an empty Ok or a NoContent.
            return Ok(yourPassword);
        }
    }
}
