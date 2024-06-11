using MercadoPago.Config;
using Microsoft.AspNetCore.Mvc;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;
using server_side.Model.DTO;
using System.Threading.Tasks;
using server_side.Data.Interfaces;
using MercadoPago.Client.Preference;
using MercadoPago; // Alias para MercadoPago

namespace server_side.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        public PaymentController()
        {
            // Configura tu Access Token
            MercadoPagoConfig.AccessToken = "APP_USR-6479854343719013-060419-7a098a06d19b3d4323cb0e8ddec19cf8-515245712";
        }

        [HttpPost("create_preference")]
        public async Task<IActionResult> CreatePreference([FromBody] PreferenceProductDTO request)
        {
            var requestPreference = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = request.Title,
                        Quantity = request.Quantity,
                        CurrencyId = "ARS",
                        UnitPrice = request.Price
                    }
                },
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "https://tu-sitio.com/success",
                    Failure = "https://tu-sitio.com/failure",
                    Pending = "https://tu-sitio.com/pending"
                },
                AutoReturn = "approved"
            };

            try
            {
                var client = new PreferenceClient();
                var preference = await client.CreateAsync(requestPreference);

                return Ok(new { id = preference.Id });
            }
            catch (Exception ex)
            {
                // Manejar otros posibles errores
                return StatusCode(500, ex.Message);
            }
        }
    }
}