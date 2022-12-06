using Redemption.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redemption.Interfaces
{
    public interface IRedemptionService
    {
        Task<Response> CreateAsync(CreateRedemption model, string email);
        Task<Response> Repayment(Repayment model, string email);
    }
}
