using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Model.Entities;
using System.Security.Claims;

namespace MiniMarket_API.Controllers
{
    [Route("api/sellers")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ICompanyService companyService;

        public SellerController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSellerAccountAsync([FromBody] CreateSellerDto createSellerDto)
        {
            var createdSeller = await companyService.CreateSeller(createSellerDto);
            if (createdSeller == null)
            {
                return Conflict ("Registration Failed: Email Currently in Use!");
            }
            return Ok(createdSeller);
        }

        [HttpPatch("{sellerId}")]
        [Authorize]
        public async Task<IActionResult> RestoreSellerAccountAsync([FromRoute] Guid sellerId)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(SuperAdmin).Name)
            {
                var restoredSeller = await companyService.RestoreSeller(sellerId);

                if (restoredSeller == null)
                {
                    return NotFound("Seller Restoration Failed: Account wasn't found, or CompanyCode is currently invalid.");
                }

                return Ok(restoredSeller);
            }

            return Forbid();
        }
    }
}
