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

        public UserAddressController()
        {
        }

        [HttpPut]
        public async Task<IActionResult> AddNewDeliveryAddressAsync([FromBody] AddDeliveryAddressDto addDeliveryAddress)
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            AddressValidation(addDeliveryAddress);

            if (ModelState.IsValid)
            {
                var newAddress = await _deliveryAddressService.AddDeliveryAddress(userId, addDeliveryAddress);

                if (newAddress != null)
                {
                    return Ok(newAddress);
                }

                return Forbid();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCurrentDeliveryAddressAsync()
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            await _deliveryAddressService.DeleteDeliveryAddress(userId);

            return NoContent();
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

        [NonAction]
        public void AddressValidation (AddDeliveryAddressDto addDeliveryAddress)
        {
            var validProvinces = new string[] { "Buenos Aires", "Ciudad Autónoma De Buenos Aires", "Catamarca", "Chaco", "Chubut",
                "Córdoba", "Corrientes", "Entre Ríos", "Formosa", "Jujuy", "La Pampa", "La Rioja", "Mendoza, Misiones",
            "Neuquén", "Rio Negro", "Salta", "San Juan", "San Luis", "Santa Cruz", "Santa Fe", "Santiago del Estero",
            "Tierra del Fuego, Antártida e Islas del Atlántico Sur", "Tucumán"};

            if (!validProvinces.Contains(addDeliveryAddress.Province)) 
            {
                ModelState.AddModelError("province", "Address Generation Failed: Please provice a valid Province.");
            }
        }
    }
}
