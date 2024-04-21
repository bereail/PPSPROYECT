using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_Server_dev.Application.DTOs.Requests;
using MiniMarket_Server_dev.Application.Services.Interfaces;

namespace MiniMarket_Server_dev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyManagementController : ControllerBase
    {
        private readonly ICompanyService companyService;

        public CompanyManagementController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        //[HttpGet("Sellers")]
        //public async Task<IActionResult> GetAllSellersAsync()
        //{
        //    var getSellers = await companyService.GetAllSellers();
        //    if (getSellers == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(getSellers);
        //}

        [HttpGet("Codes")]
        public async Task<IActionResult> GetAllCodesAsync()
        {
            var getCodes = await companyService.GetAllCompanyCodes();
            if (getCodes == null)
            {
                return NotFound();
            }
            return Ok(getCodes);
        }

        [HttpPost("Create Codes")]
        public async Task<IActionResult> CreateCompanyCodeAsync([FromBody] AddCompanyCodeDto addCompanyCodeDto)
        {
            var createdCode = await companyService.CreateCompanyCode(addCompanyCodeDto);
            if (createdCode == null)
            {
                return BadRequest();
            }
            return Ok(createdCode);
        }

        [HttpPost("Create Seller")]
        public async Task<IActionResult> CreateSellerAccountAsync([FromBody] CreateSellerDto createSellerDto)
        {
            var createdSeller = await companyService.CreateSeller(createSellerDto);
            if (createdSeller == null)
            {
                return BadRequest();
            }
            return Ok(createdSeller);
        }

        [HttpDelete("Deactivate Code/{codeId}")]
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
