using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using System.Security.Claims;

namespace MiniMarket_API.Controllers
{
    [Route("api/users/addresses")]
    [ApiController]
    [Authorize]
    public class UserAddressController : ControllerBase
    {
        private readonly IDeliveryAddressService _deliveryAddressService;
        public UserAddressController(IDeliveryAddressService deliveryAddressService)
        {
            _deliveryAddressService = deliveryAddressService;
        }

        [HttpPut]
        public async Task<IActionResult> AddNewDeliveryAddressAsync([FromBody] AddDeliveryAddressDto addDeliveryAddress)
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            var newAddress = await _deliveryAddressService.AddDeliveryAddress(userId, addDeliveryAddress);

            if (newAddress != null)
            {
                return Ok(newAddress);
            }

            return Forbid();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCurrentDeliveryAddressAsync()
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            var deletedAddress = await _deliveryAddressService.DeleteDeliveryAddress(userId);

            if (deletedAddress != null)
            {
                return Ok(deletedAddress);
            }
            return NotFound("Address Deletion Failed: You have no current Address.");
        }

        [HttpGet]
        public async Task<IActionResult> GetMyCurrentAddressAsync()
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            var getAddress = await _deliveryAddressService.GetDeliveryAddressByUserId(userId);

            if (getAddress != null)
            {
                return Ok(getAddress);
            }

            return NotFound("You have no current Address.");
        }
    }
}
