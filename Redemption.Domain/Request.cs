using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redemption.Domain
{
    public class Request
    {
        public string TransactionId { get; set; }
        public string DocDate { get; set; }
        public string ValDate { get; set; }
        public decimal Amount { get; set; }
        public string? AccountSender { get; set; }
        public string? AccountRecepient { get; set; }
        public string? BicBankRecepient { get; set; }
        public string? KnpCode { get; set; }
        public string? Purpose { get; set; }
        public string? NameRecepient { get; set; }
        public string? RnnRecepient { get; set; }
    }
}
