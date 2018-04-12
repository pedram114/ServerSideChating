using System.Collections.Generic;

namespace Common.UsingModels
{
    public static class SendingMessageQueue
    {

        public static readonly Queue<ReceiveMessage> SendingMessages = new Queue<ReceiveMessage>();
        public static readonly Queue<ReceiveMessage> NotConnectClientSendingMessages = new Queue<ReceiveMessage>();
    }
}