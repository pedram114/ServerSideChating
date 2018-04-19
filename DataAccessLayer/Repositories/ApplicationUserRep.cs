using System.Linq;
using Common.Model;

namespace DataAccessLayer.Repositories
{
    public class ApplicationUserRep
    {
        private readonly PedramDbContext _context;
        public ApplicationUserRep(PedramDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetUserByUserName(string UserName)
        {
            return _context.AspNetUsers.FirstOrDefault(u => u.UserName == UserName);

        }
    }
}