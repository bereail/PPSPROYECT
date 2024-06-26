﻿using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Model.Entities;
using System.Diagnostics;

namespace MiniMarket_API.Application.Services.Implementations
{
    public class MpOrderService : IMpOrderService
    {
        public async Task<string> HandlePreferenceRequest(ICollection<OrderDetails> details)
        {
            MercadoPagoConfig.AccessToken = "APP_USR-6479854343719013-060419-7a098a06d19b3d4323cb0e8ddec19cf8-515245712";

            var preferenceItems = new List<PreferenceItemRequest>();

            foreach (var detail in details)
            {
                var preferenceDetail = new PreferenceItemRequest
                {
                    Title = $"{detail.ProductName}" + " X " + $"{detail.ProductQuantity}",
                    UnitPrice = detail.DetailPrice,
                    //Might want to change this to a set amount of 1
                    Quantity = 1,
                    CurrencyId = "ARS",
                };
                preferenceItems.Add(preferenceDetail);
            }

            var requestPreference = new PreferenceRequest
            {
                BinaryMode = true,

                Items = preferenceItems,

                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "http://localhost:3000/paySucess",
                    Failure = "http://localhost:3000/User",
                    Pending = "http://localhost:3000/User"
                },
                AutoReturn = "approved"
            };
            Trace.WriteLine(requestPreference);

            var client = new PreferenceClient();
            var preference = await client.CreateAsync(requestPreference);

            return preference.Id;
        }
    }
}
