using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Model.Entities;
using MiniMarket_API.Model.Enums;
using System.Security.Claims;

namespace MiniMarket_API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
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
            //We retrieve the user's ID from the claims directly.
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != typeof(Customer).Name)
            {
                return Forbid();
            }

            var createdOrder = await _saleOrderService.CreateSaleOrder(createOrder, userId);        
            if (createdOrder == null)
            {
                return Unauthorized("Order Creation Failed: User Doesn't Exist or Lacks a Valid Address.");
            }
            return Ok(createdOrder);
        }

        [HttpGet]
        //FOR ADMIN ONLY. This is a test endpoint for enum functionality.
        public async Task<IActionResult> GetAllOrdersAsync([FromQuery] OrderStatus? status, [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 7)
        {
            var getOrders = await _saleOrderService.GetAllOrders(status, sortBy, isAscending, pageNumber, pageSize);

            if (getOrders == null || !getOrders.Any()) 
            {
                return NotFound("No Orders Found");
            }
            return Ok(getOrders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderByIdAsync([FromRoute] Guid orderId)
        {
            var getOrder = await _saleOrderService.GetOrderById(orderId);

            if (getOrder == null)
            {
                return NotFound();
            }

            return Ok(getOrder);
        }

        [HttpGet("timeframe")]
        //FOR ADMIN ONLY.
        public async Task<IActionResult> GetAllOrdersByTimeframeAsync([FromQuery] int filterDays, [FromQuery] OrderStatus? status, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 7)
        {
            var getOrders = await _saleOrderService.GetAllOrdersByTimeframe(filterDays, status, sortBy, isAscending, pageNumber, pageSize);

            if (getOrders == null || !getOrders.Any())
            {
                return NotFound("No Orders Found");
            }
            return Ok(getOrders);
        }
    }
}
