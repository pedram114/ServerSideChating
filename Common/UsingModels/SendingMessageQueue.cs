using System.Collections.Generic;

namespace Common.UsingModels
{
    public static class SendingMessageQueue
    {
        public static readonly Queue<MessageUM> SendingMessages = new Queue<MessageUM>();
        public static readonly Queue<MessageUM> NotConnectClientSendingMessages = new Queue<MessageUM>();
    }
}