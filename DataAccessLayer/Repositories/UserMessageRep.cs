using System.Linq;
using Common.Enums;
using Common.Model;

namespace DataAccessLayer.Repositories
{
    public class UserMessageRep
    {
        private readonly PedramDbContext _context;
        private readonly ApplicationUserRep _applicationUserRep;
        
        public UserMessageRep(PedramDbContext context)
        {
            _context = context;
            _applicationUserRep = new ApplicationUserRep(_context);

        }

        public IQueryable<UserMessage> GetInboxByUserName(string userName)
        {
            var user = _applicationUserRep.GetUserByUserName(userName);
            return user.UserMessages;

        }
        
        public DBResponse AddMessageToInbox(string userName,UserMessage message)
        {
            var response=new DBResponse();
            var user = _applicationUserRep.GetUserByUserName(userName);
            if (user == null)
            {
                response.IsSucced = false;
                response.Messages.Add("user not exist!");
                return response;
            }
            message.InboxOfUser = user;
            _context.userMessages.Add(message);
            var resp = _context.SaveChanges();
            if (resp != 1)
            {
                response.IsSucced = false;
                response.Messages.Add("Data cannot insert to database!");                
            }          
            return response;


        }
    }
}