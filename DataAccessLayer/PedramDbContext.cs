using Common.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class PedramDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,long>//, DbContext
    {
        public PedramDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Client> Clients { set; get; }
        public DbSet<UserMessage> UserMessages { set; get; }
        public DbSet<ApplicationUser> AspNetUsers { set; get; }
        public DbSet<MessageReceipter> MessageReceipters { set; get; }
   
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"Server=localhost;Database=ChatDB;Trusted_Connection=True;MultipleActiveResultSets=true;Uid=sa;Pwd=12salam12;");
        }
        
      
     
    }
}