using Common.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class PedramDbContext : IdentityDbContext//, DbContext
    {
        public PedramDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> clients { set; get; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"Server=localhost;Database=ChatDB;Trusted_Connection=True;MultipleActiveResultSets=true;Uid=sa;Pwd=12salam12;");
        }
    }
}