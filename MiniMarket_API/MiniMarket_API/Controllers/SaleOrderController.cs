using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using System.Security.Claims;

namespace MiniMarket_API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class SaleOrderController : ControllerBase
    {
        private readonly ISaleOrderService _saleOrderService;

        public SaleOrderController(ISaleOrderService saleOrderService)
        {
            _saleOrderService = saleOrderService;
        }

        [HttpPost]
        //Once authorization requirements are enforced, remove the Guid 'userId' param from the IActionResult method, and uncomment the claims retrieval.
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderDto createOrder, Guid userId)
        {
            //var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            var createdOrder = await _saleOrderService.CreateSaleOrder(createOrder, userId);        //This is to prevent users sending IDs that aren't theirs.
            if (createdOrder == null)
            {
                return Unauthorized("Order Creation Failed: User Doesn't Exist!");
            }
            return Ok(createdOrder);
        }
    }
}
