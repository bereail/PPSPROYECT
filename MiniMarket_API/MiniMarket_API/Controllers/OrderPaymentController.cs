﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.Services.Interfaces;
using System.Security.Claims;

namespace MiniMarket_API.Controllers
{
    [Route("api/orders/{orderId}/payment")]
    [ApiController]
    [Authorize]
    public class OrderPaymentController : ControllerBase
    {
        private readonly ISaleOrderService _orderService;

        public OrderPaymentController(ISaleOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateNewPreferencesAsync([FromRoute] Guid orderId)
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            var preferenceData = await _orderService.HandlePaymentRequest(orderId, userId);

            if (preferenceData == null)
            {
                return BadRequest("Preference Creation Failed: Order wasn't found or is innacessible");
            }
            return Ok(preferenceData);
        }

        [HttpPost("success")]
        // For now remains a simple implementation to get around MP's localhost limitations
        public async Task<IActionResult> HandleSuccessfulPaymentAsync([FromRoute] Guid orderId)
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            var paidOrder = await _orderService.SetPaidOrderStatus(orderId, userId);

            if (paidOrder == null)
            {
                return Forbid();
            }

            return Ok(paidOrder);
        }
    }
}
