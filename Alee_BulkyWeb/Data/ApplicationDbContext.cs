using Alee_BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace Alee_BulkyWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }

    }
}
