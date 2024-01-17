using Microsoft.EntityFrameworkCore;

namespace PortHub.Api.Users.Data
{
    public class appDbContext : DbContext
    {
        public appDbContext(DbContextOptions<appDbContext> options) : base(options)
        {
            
        }
    }
}
