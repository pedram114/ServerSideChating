using System;
using System.Text;
using Common.UsingModels;

namespace Business.SharedClasses
{
    public static class MessageHandle
    {
        private const int FromIdLength = 20;
        private const int ToIdLength = 20;

        public static byte[] PrepareDataforSending(Message msg,Encoding encoding)
        {
            try
                       {
                           var frmIdlength = msg.FromId.Length;
                           for (var i = 0; i < FromIdLength - frmIdlength; i++)
                           {
                               msg.FromId = msg.FromId.Insert(0, "0");
                           }
                           var toIdlength = msg.ToId.Length;              
                           for (var i = 0; i < ToIdLength - toIdlength; i++)
                           {
                               msg.ToId = msg.ToId.Insert(0, "0");
                           }
                           var NewMsg = msg.FromId + msg.ToId + msg.Text;
                           return encoding.GetBytes(NewMsg);
                       }
                       catch (Exception e)
                       {
                           Console.WriteLine("Error In PreaparingData Method : " + e.Message);
                           return null;
                       }
        }

        public static Message PrepareReceivedData(byte[] text,Encoding _Encoding)
        {
            Message GotMessage = new Message();
            try
            {

                var data = Decrypt(_Encoding.GetString(text));
                GotMessage.FromId = data.Substring(0, FromIdLength);
                GotMessage.ToId = data.Substring(FromIdLength, ToIdLength);
                GotMessage.Text = data.Substring(FromIdLength + ToIdLength, (data.Length - (FromIdLength + ToIdLength)));
                return GotMessage;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error In PreaparingData Method : " + e.Message);
                return null;
            }
        }

        public static string Decrypt(string text)
        {
            return text;
        }

        public static string Encrypt(string text)
        {
            return text;
        }
        
       
    }
}