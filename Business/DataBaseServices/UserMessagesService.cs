using Common.Model;
using Common.UsingModels;
using DataAccessLayer;
using DataAccessLayer.Repositories;

namespace Business.DataBaseServices
{
    public class UserMessagesService
    {
        private readonly PedramDbContext _context;
        private readonly UserMessageRep _userMessageRep;
        public UserMessagesService(PedramDbContext context)
        {
            _context = context;
            _userMessageRep=new UserMessageRep(_context);
        }

        public void InsertMessageToInbox(MessageUM message)
        {
            _userMessageRep.AddMessageToInbox(message);
        }

    }
}