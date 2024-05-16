using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;

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
    }
}
