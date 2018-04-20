using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Common.Model;
using Common.UsingModels;

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

        public List<UserMessage> GetInboxByUserName(string userName)
        {
            var user = _applicationUserRep.GetUserByUserName(userName);
            return user.UserMessages.ToList();

        }
        
        public DBResponse AddMessageToInbox(MessageUM newmessage)
        {
            
            var response=new DBResponse();
            var fromuser = _applicationUserRep.GetUserByUserName(newmessage.FromId);
            var touser = _applicationUserRep.GetUserByUserName(newmessage.ToId);


            var receiptor = new List<MessageReceipter>()
            {
                new MessageReceipter()
                {
                    ReceipterUser = touser,
                }
            };
            var message = new UserMessage()
            {
                SendDate = newmessage.SendDate,
                ReadDate = newmessage.ReadDate,
                Receivedate = newmessage.ReceiveDate,
                 MessageReceipters = receiptor
            };
            
             if (fromuser == null || touser == null)
            {
                response.IsSucced = false;
                response.Messages.Add("user not exist!");
                return response;
            }
            message.FromUser = fromuser;


            _context.UserMessages.Add(message);
            var resp = _context.SaveChanges();

//            response.IsSucced = false;
//            response.Messages.Add("Data cannot insert to database!");             
            return response;


        }
    }
}