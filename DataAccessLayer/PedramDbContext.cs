using Common.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class PedramDbContext : DbContext
    {
        public PedramDbContext(DbContextOptions options) : base(options)
        {
            
            
        }

        public DbSet<Client> clients { set; get; }

      
        protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         
            // optionsBuilder.UseSqlServer(@"Server=localhost;Database=ChatDB;Trusted_Connection=True;MultipleActiveResultSets=true;Uid=sa;Pwd=12salam12;");
        }
    }
}