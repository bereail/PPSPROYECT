using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;

namespace MiniMarket_API.Controllers
{
    [Route("api/users/{userdId}/orders")]
    [ApiController]
    public class SaleOrderController : ControllerBase
    {
        private readonly ISaleOrderService _saleOrderService;

        public SaleOrderController(ISaleOrderService saleOrderService)
        {
            _saleOrderService = saleOrderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderDto createOrder)
        {
            var createdOrder = await _saleOrderService.CreateSaleOrder(createOrder);
            if (createdOrder == null)
            {
                return BadRequest();
            }
            return Ok(createdOrder);
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrderAsync([FromRoute] Guid orderId, [FromBody] UpdateOrderDto updateOrder)
        {
            var updatedOrder = await _saleOrderService.UpdateSaleOrder(orderId, updateOrder);
            if (updatedOrder == null)
            {
                return BadRequest("Couldn't update order");
            }
            return Ok(updatedOrder);
        }
    }
}
