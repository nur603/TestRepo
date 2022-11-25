using Microsoft.AspNetCore.Http;
using Redemption.Domain;
using Redemption.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Redemption.Infrastructure
{
    public class RedemptionService : IRedemptionService
    {
        public async Task<Response> CreateAsync(CreateRedemption model)
        {
            string json = JsonSerializer.Serialize(model);
            var uri = new Uri("https://localhost:7063/Account/test");
                        using (var client = new HttpClient())
            {
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                //stringContent.Headers.Add("Authorization", "b04930add2304e408811da62195d23ca");
                HttpResponseMessage message = await client.PostAsync(uri, stringContent);
                var stringResponse = await message.Content.ReadAsStringAsync();
            }
            return null;
        }
    }
}
