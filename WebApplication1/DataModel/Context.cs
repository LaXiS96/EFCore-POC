using Microsoft.EntityFrameworkCore;

namespace WebApplication1.DataModel
{
    public class MyContext : DbContext
    {
        public DbSet<Entity> Entities { get; set; }

        public MyContext(DbContextOptions options) : base(options)
        {
        }
    }
}
