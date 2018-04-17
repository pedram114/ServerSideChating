using System.Collections.Generic;

namespace Common.UsingModels
{
    public static class SendingMessageQueue
    {
        public static readonly Queue<Message> SendingMessages = new Queue<Message>();
        public static readonly Queue<Message> NotConnectClientSendingMessages = new Queue<Message>();
    }
}