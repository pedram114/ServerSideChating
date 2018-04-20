using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Business.ServerSideSocketClasses;
using Common.Enums;
using Common.UsingModels;

namespace Business.SharedClasses
{
    public static class MessageHandle
    {
        private const int FromIdLength = 30;
        private const int ToIdLength = 30;        

        public static byte[] PrepareDataforSending(MessageUM msg,Encoding encoding,MySocketType sockettype,string webSocketSecKey)
        {
            try
                       {             
                           if (sockettype != MySocketType.Websocket) 
                               return encoding.GetBytes(msg.Text);
                                              
                           var lb = new List<byte> {0x81, (byte) msg.Text.Length};
                           lb.AddRange(Encoding.UTF8.GetBytes(msg.Text));                               
                           return lb.ToArray();
                       }
                       catch (Exception e)
                       {
                           Console.WriteLine("Error In PreaparingData Method : " + e.Message);
                           return null;
                       }
        }

        public static MessageUM PrepareReceivedData(byte[] text,Encoding _Encoding)
        {
            var gotMessage = new MessageUM();
            try
            {      
                var data = Decrypt(_Encoding.GetString(text));


                if (new System.Text.RegularExpressions.Regex("websocket").IsMatch(data))
                {
                    var length = data.Substring(0, data.IndexOf("\r\n")).Length;
                    data = GetDecodedData(text.Take(length).ToArray(),length);                    
                }

                if (data.Trim() == "?")
                    gotMessage.Text = "?";
    
                if (data.Length < FromIdLength + ToIdLength)
                    return gotMessage;
                gotMessage = MessageProtocol.getDecodedMessage(data);
                return gotMessage;
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

        private static string GetDecodedData(byte[] buffer, int length)
        {
            var b = buffer[1];
            var dataLength = 0;
            var totalLength = 0;
            var keyIndex = 0;

            if (b - 128 <= 125)
            {
                dataLength = b - 128;
                keyIndex = 2;
                totalLength = dataLength + 6;
            }

            if (b - 128 == 126)
            {
                dataLength = BitConverter.ToInt16(new byte[] { buffer[3], buffer[2] }, 0);
                keyIndex = 4;
                totalLength = dataLength + 8;
            }

            if (b - 128 == 127)
            {
                dataLength = (int)BitConverter.ToInt64(new byte[] { buffer[9], buffer[8], buffer[7], buffer[6], buffer[5], buffer[4], buffer[3], buffer[2] }, 0);
                keyIndex = 10;
                totalLength = dataLength + 14;
            }

            if (totalLength > length)
                throw new Exception("The buffer length is small than the data length");

            byte[] key = new byte[] { buffer[keyIndex], buffer[keyIndex + 1], buffer[keyIndex + 2], buffer[keyIndex + 3] };

            int dataIndex = keyIndex + 4;
            int count = 0;
            for (int i = dataIndex; i < totalLength; i++)
            {
                buffer[i] = (byte)(buffer[i] ^ key[count % 4]);
                count++;
            }

            return Encoding.ASCII.GetString(buffer, dataIndex, dataLength);
        }

    }
}