using AuthService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Entities
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
