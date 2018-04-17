using System.Linq;
using System.Threading;
using Business.Interfaces;
using Common.UsingModels;

namespace Business.ServerSideSocketClasses
{
    public class SendingMessagesHandle : ISendingMessagesHandle
    {
        public void CheckMessageQueue()
        {
            while (true)
            {
                if (SendingMessageQueue.SendingMessages.Count <= 0)
                {
                    Thread.Sleep(5000);
                    continue;
                }

                var currentMessage = SendingMessageQueue.SendingMessages.Peek();
                var currentSocket = TCPListener.Allclients.FirstOrDefault(m => m.ClientId == currentMessage.ToId);
                if (currentSocket == null)
                {
                    SendingMessageQueue.NotConnectClientSendingMessages.Enqueue(SendingMessageQueue.SendingMessages
                        .Dequeue());
                }
                else
                {
                    SendingMessageQueue.SendingMessages.Dequeue();
                    currentSocket.SendingMessage = currentMessage;
                    new Thread(Sendmessage).Start(currentSocket);
                }
            }
        }

        private void Sendmessage(object currentSocket)
        {
            var thissocket = (ClientSocket) currentSocket;
            var sendMessage = thissocket.SendMessage(thissocket.SendingMessage);
        }
    }
}