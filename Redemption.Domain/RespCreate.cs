using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redemption.Domain
{
    public class RespCreate
    {
        public string RespClass { get; set; }
        public int RespCode { get; set; }
        public string? RespText { get; set; }
        public string? PostingStatus { get; set; }
    }
}
