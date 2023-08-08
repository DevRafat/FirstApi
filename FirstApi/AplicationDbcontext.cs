using FirstApi.Tables;
using Microsoft.EntityFrameworkCore;

namespace FirstApi
{
    public class AplicationDbContext:DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> option ):base(option)
        {
            
        }
        public DbSet<Major>Majors { get; set; }
    }
}
