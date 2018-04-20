using System;
using System.Linq;
using System.Threading;
using Business.Interfaces;
using Common.UsingModels;
using DataAccessLayer;
using DataAccessLayer.Repositories;

namespace Business.ServerSideSocketClasses
{
    public class SendingMessagesHandle : ISendingMessagesHandle
    {
        private readonly PedramDbContext _context;
        private readonly UserMessageRep _userMessageRep;
        public SendingMessagesHandle(PedramDbContext context)
        {
            _context = context;
            _userMessageRep = new UserMessageRep(_context);
        }
        public void CheckMessageQueue()
        {
            while (true)
            {
                if (SendingMessageQueue.SendingMessages.Count <= 0)
                {
                    Thread.Sleep(3000);
                    continue;
                }
                
                var currentSocket = TCPListener.Allclients.FirstOrDefault(m => m.ClientId == SendingMessageQueue.SendingMessages.Peek().ToId);
                if (currentSocket == null)
                {
//                    SendingMessageQueue.SendingMessages
//                        .Dequeue().Sendtry++;

//                    if (SendingMessageQueue.SendingMessages.Peek().Sendtry > 100)
//                    {
//
//                        SendingMessageQueue.NotConnectClientSendingMessages.Enqueue(SendingMessageQueue.SendingMessages
//                            .Dequeue());
//                    }
//                    else
//                    {
//                        SendingMessageQueue.SendingMessages.Enqueue(SendingMessageQueue.SendingMessages
//                            .Dequeue());
                    Thread.Sleep(2000);
                    //}
                }
                else
                {
                    currentSocket.SendingMessage = SendingMessageQueue.SendingMessages.Peek();                    
                    new Thread(Sendmessage).Start(currentSocket);
                    _userMessageRep.AddMessageToInbox(currentSocket.SendingMessage);
                    SendingMessageQueue.SendingMessages.Dequeue();
                }
            }
        }

        private void Sendmessage(object currentSocket)
        {
            var thissocket = (ClientSocket) currentSocket;

            try
            {
                var sendMessage = thissocket.SendMessage(thissocket.SendingMessage);

            }
            catch (Exception e)
            {
                SendingMessageQueue.SendingMessages.Enqueue(thissocket.SendingMessage);                
            }
            
            
        }
    }
}