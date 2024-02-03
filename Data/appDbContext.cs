using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortHub.Api.Users.Data.Models;

namespace PortHub.Api.Users.Data
{
    public class appDbContext : IdentityDbContext<ApplicationUser>
    {
        public appDbContext(DbContextOptions<appDbContext> options) : base(options)
        {
            
        }
        public DbSet<ApplicationUser> app {  get; set; }
    }
}
