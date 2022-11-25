using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redemption.Domain
{
    public class CreateRedemption
    {
        public string Company { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
    }
}
