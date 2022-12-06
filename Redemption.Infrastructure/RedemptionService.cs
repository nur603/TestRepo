using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Redemption.Domain;
using Redemption.Interfaces;
using Redemption.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Redemption.Infrastructure
{
  
    public class RedemptionService : IRedemptionService
    {
        private readonly RedemptionContext _context;
        private readonly IConfiguration _config;
        public RedemptionService(RedemptionContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task<Response> CreateAsync(CreateRedemption model,string email)
        {
            model.Currency = "KZT";
            string json = JsonSerializer.Serialize(model);
            string stringResponse;
            var uri = new Uri("https://localhost:7063/Account/test");
                        using (var client = new HttpClient())
            {
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                //stringContent.Headers.Add("Authorization", "b04930add2304e408811da62195d23ca");
                HttpResponseMessage message = await client.PostAsync(uri, stringContent);
                stringResponse = await message.Content.ReadAsStringAsync();
            }
            var response = JsonSerializer.Deserialize<RespCreate>(stringResponse);
            var user = _context.Users.Where(w => w.Email == email).FirstOrDefault();
            var log = new Log {
            UserId = user.Id,
            CreatedDate = DateTime.Now,
            ResultCode = response.RespCode.ToString(),
            Message = response.RespText
            };
            _context.Logs.Add(log);
            _context.SaveChanges();
            return new Response {Value = "ok" };
        }

        public Task<Response> Repayment(Repayment model,string email)
        {
            var json = SerializeJson(model);
            //
            //switch (model.Company)
            //{
            //    case "RPS1":
            //        return null;
            //    case "RPS2":
            //        return null;
            //    case "Jassefy":
            //        return null;
            //        break;
            //    default:
            //        return null;
            //        break;
            //}
            var result = RepaymentGet(json);
            //XmlSerializer serializer = new XmlSerializer(typeof(MakePayOrderResponse));
            //using (StringReader reader = new StringReader(result))
            //{
            //    result = (MakePayOrderResponse)serializer.Deserialize(reader);
            //}

            var log = new Log 
            {
                CreatedDate = DateTime.Now.ToLocalTime(),
                UserId = email,
                
            };
            return null;
        }
        //private void CreateLog(Repayment model, string email)
        //{
        //    var user =_context.Users.FirstOrDefault(s=>s.Email == email);
        //    var log = new Log
        //    {
        //        UserId = user.Id,
        //        CreatedDate = DateTime.Now.ToLocalTime(),
                
        //    };
        //    _context.Logs.Add(log);
        //    _context.SaveChanges();
        //}
        private async Task<string> RepaymentGet(string json)
        {
            var uri = new Uri("https://localhost:7063/Account/test");
            string stringResponse;
            using (var client = new HttpClient())
            {
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                //stringContent.Headers.Add("Authorization", "b04930add2304e408811da62195d23ca");
                HttpResponseMessage message = await client.PostAsync(uri, stringContent);
                stringResponse = await message.Content.ReadAsStringAsync();
            }
            return stringResponse;
        }
        private string SerializeJson(Repayment model) 
        {

            var request = new Request {
                TransactionId = _context.Logs.OrderBy(t=>t.TransactionId != null).Select(t=>t.TransactionId).ToString(),
                DocDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ValDate = DateTime.Now.ToString("yyyy-MM-dd"),
                Amount = model.Amount,
            };
            if (model.Company == "RPS1")
            {
                request.AccountSender = _config["Comission:AccountSender"];
                request.AccountRecepient = _config["Comission:AccountRecepient"];
                request.BicBankRecepient = _config["Comission:BicBankRecepient"];
                request.KnpCode = _config["Comission:KnpCode"];
                request.Purpose = _config["Comission:Purpose"];
            }
            else if (model.Company == "RPS2")
            {
                request.AccountSender = _config["Main:AccountSender"];
                request.AccountRecepient = _config["Main:AccountRecepient"];
                request.BicBankRecepient = _config["Main:BicBankRecepient"];
                request.KnpCode = _config["Main:KnpCode"];
                request.Purpose = _config["Main:Purpose"];
            }
            else if (model.Company == "Jassefi")
            {
                request.AccountSender = _config["Jassefi:AccountSender"];
                request.AccountRecepient = _config["Jassefi:AccountRecepient"];
                request.BicBankRecepient = _config["Jassefi:BicBankRecepient"];
                request.KnpCode = _config["Jassefi:KnpCode"];
                request.Purpose = _config["Jassefi:Purpose"];
                request.NameRecepient = _config["Jassefi:NameRecepient"];
                request.RnnRecepient = _config["Jassefi:RnnRecepient"];
                
            }
            return JsonSerializer.Serialize(request);
        }
        
    }
}
