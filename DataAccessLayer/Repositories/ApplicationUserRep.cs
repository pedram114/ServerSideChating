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
            var data = from query in _context.AspNetUsers
                    where query.UserName == UserName
                    select query;
                var user = data.FirstOrDefault();
          
            return user;

        }
    }
}