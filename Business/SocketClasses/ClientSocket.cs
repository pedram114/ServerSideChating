using System;
using System.Net.Sockets;
using System.Text;
using Common.UsingModels;

namespace Business.SocketClasses
{
    public class ClientSocket
    {
        #region Properties

        private Socket _ClientSocket { set; get; }
        private Encoding Encoding { get; set; }
        public string ClientId { set; get; }
        public string SendingMessage { set; get; }

        private string ClientIp { set; get; }
        private readonly byte[] _readbuffer=new byte[2000];
        private string _readMessagestr = "";

        
        #endregion

        #region CTOR

        public ClientSocket(Socket socket,Encoding encoding)
        {
            _ClientSocket = socket;
            Encoding = encoding;
            ClientIp = socket.AddressFamily.ToString();

        }
        #endregion


        public void WaitforData()
        {
            _ClientSocket.BeginReceive(_readbuffer, 0, _readbuffer.Length, SocketFlags.None,
                new AsyncCallback(ReadMessageAync),_ClientSocket);
            
        }

        public bool IsConnected() {
            try
            {
                return _ClientSocket.Connected;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error In IsConnected Method : " + e.Message);
                return false;
            }
        
        }
        public Response SendMessage( string text) {
            try
            {
                var sendingdata = PrepareDataforSending(text);
                _ClientSocket.Send(sendingdata);
                return new Response() {
                    code = Common.Enums.ResponseStatus.Sent,
                    text = ""
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Error In SendMessage Method : " + e.Message);
                return new Response()
                {
                    code = Common.Enums.ResponseStatus.CanNotSend,
                    text = e.Message
                };
            }
         
        }

        private void ReadMessageAync(IAsyncResult ar)
        {
            var sock = (Socket) ar.AsyncState;
            try
            {
                var countread = sock.EndReceive(ar);
                if (countread > 0)
                {
                    _readMessagestr = PrepareReceivedData(_readbuffer);
                    var message = DefineReceivedMessage(_readMessagestr);
                    SendingMessageQueue.SendingMessages.Enqueue(message);
                }
              
            }
            catch (Exception e)
            {
                Console.WriteLine("Error In SendMessage Method : " + e.Message);
                
            }
           // sock.EndConnect(ar);
            WaitforData();
        }
        #region Private Methods
        private byte[] PrepareDataforSending(string text) {
            try
            {
                return Encoding.GetBytes(text);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error In PreaparingData Method : " + e.Message);
                return null;
            }
          
        }
        private string PrepareReceivedData(byte[] text)
        {
            try
            {
                return Decrypt(Encoding.GetString(text));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error In PreaparingData Method : " + e.Message);
                return null;
            }

        }

        private static string Decrypt(string text)
        {
            return text;

        }
        private static string Encrypt(string text)
        {
            return text;

        }

        private  ReceiveMessage DefineReceivedMessage(string text)
        {
            var splitedtext = text.Split("%#09e");
            return new ReceiveMessage() {FromId = splitedtext[0], ToId = splitedtext[1], Text = splitedtext[2]};

        }

        #endregion

    }
}
