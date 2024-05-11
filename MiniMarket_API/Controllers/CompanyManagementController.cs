using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;

namespace MiniMarket_API.Controllers
{
    [Route("api/codes")]
    [ApiController]
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
            var getCodes = await companyService.GetAllCompanyCodes();
            if (getCodes == null)
            {
                return NotFound();
            }
            return Ok(getCodes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanyCodeAsync([FromBody] AddCompanyCodeDto addCompanyCodeDto)
        {
            var createdCode = await companyService.CreateCompanyCode(addCompanyCodeDto);
            if (createdCode == null)
            {
                return BadRequest();
            }
            return Ok(createdCode);
        }

        [HttpDelete("{codeId}")]
        public async Task<IActionResult> DeactivateCompanyCodeAsync([FromRoute] Guid codeId)
        {
            var deactivatedCode = await companyService.DeactivateCompanyCode(codeId);
            if (deactivatedCode == null)
            {
                return BadRequest();
            }
            return Ok(deactivatedCode);
        }
    }
}
