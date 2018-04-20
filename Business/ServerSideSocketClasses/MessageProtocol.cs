using System;
using Common.UsingModels;
using StackExchange.Redis;

namespace Business.ServerSideSocketClasses
{
    public static class MessageProtocol
    {
        private static string FUN { get; set; }
        private static string TUN { get; set; }
        private static int FUNL { get; set; }
        private static int TUNL { get; set; }
        private static string Text { get; set; }
        private static int TL { get; set; }
        private static string prepairedMessage { get; set; }

        private static string protpcol = "FUNL,TUNL,TL,FUNTUNText";

//        public MessageProtocol(MessageUM ms)
//        {
//            this.FUN = ms.FromId;
//            this.TUN = ms.ToId;
//            this.Text = ms.Text;
//            this.FUNL = ms.FromId.Length;
//            this.TUNL = ms.ToId.Length;
//            this.TL = ms.Text.Length;
//            prepairedMessage = string.Format(this.FUNL.ToString(), ',', this.FUNL.ToString(), ',', this.TL.ToString(),
//                this.FUN, this.TUN, this.Text);            
//        }

        public static string GetPreapairedMessage()
        {
            return prepairedMessage;
        }

        public static MessageUM getDecodedMessage(string ms)
        {

            var messagesplited = ms.Split(',');
            FUNL = int.Parse(messagesplited[0]);
            TUNL = int.Parse(messagesplited[1]);
            TL = int.Parse(messagesplited[2]);
            FUN = messagesplited[3].Substring(0, FUNL);
            TUN = messagesplited[3].Substring(FUNL, TUNL);
            Text = messagesplited[3].Substring(FUNL + TUNL, TL);


            return new MessageUM()
            {
                FromId = FUN,
                ToId = TUN,
                Text = Text
            };
        }
    }
}