using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Redemption.Models.Data
{
    public class RedemptionContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }
        public RedemptionContext(DbContextOptions<RedemptionContext> options) : base(options)
        {
        }

      
    }
}
