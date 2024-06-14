using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Model.Entities;
using System.Security.Claims;

namespace MiniMarket_API.Controllers
{
    [Route("api/codes")]
    [ApiController]
    [Authorize]
    public class CompanyManagementController : ControllerBase
    {
        private readonly ICompanyService companyService;

        public CompanyManagementController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCodesAsync()
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(SuperAdmin).Name)
            {
                var getCodes = await companyService.GetAllCompanyCodes();
                if (getCodes == null)
                {
                    return NotFound();
                }
                return Ok(getCodes);
            }

            return Forbid();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanyCodeAsync([FromBody] AddCompanyCodeDto addCompanyCodeDto)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(SuperAdmin).Name)
            {
                var createdCode = await companyService.CreateCompanyCode(addCompanyCodeDto);
                if (createdCode == null)
                {
                    return Conflict("Code Creation Failed: Code Already Exists!");
                }
                return Ok(createdCode);
            }

            return Forbid();
        }

        [HttpDelete("{codeId}")]
        public async Task<IActionResult> DeactivateCompanyCodeAsync([FromRoute] Guid codeId)
        {
            var deactivatedCode = await companyService.DeactivateCompanyCode(codeId);
            if (deactivatedCode == null)
            {
                return NotFound("Code Deactivation Failed: Code Couldn't be Found!");
            }
            return Ok(deactivatedCode);
        }

        [HttpDelete("{codeId}/erase")]
        public async Task<IActionResult> EraseCompanyCodeAsync([FromRoute] Guid codeId)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(SuperAdmin).Name)
            {
                await companyService.EraseCompanyCode(codeId);

                return NoContent();
            }

            return Forbid();
        }
    }
}
