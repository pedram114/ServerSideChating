using System;
using System.Net.Sockets;
using System.Text;
using Business.SharedClasses;
using Common.Enums;
using Common.UsingModels;

namespace Business.ServerSideSocketClasses
{
    public class ClientSocket
    {
        #region Properties

        private Socket _ClientSocket { get; }
        private Encoding _Encoding { get; }
        public string ClientId { set; get; }
        public Message SendingMessage { set; get; }

        private string ClientIp { get; }
        private readonly byte[] _readbuffer = new byte[2000];
        private string _readMessagestr = "";

        #endregion

        
        
        #region CTOR

        public ClientSocket(Socket socket, Encoding encoding)
        {
            _ClientSocket = socket;
            _Encoding = encoding;
            ClientIp = socket.AddressFamily.ToString();
        }

        #endregion


        public void WaitforData()
        {
            _ClientSocket.BeginReceive(_readbuffer, 0, _readbuffer.Length, SocketFlags.None,
                ReadMessageAync, _ClientSocket);
        }

        public bool IsConnected()
        {
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

        public Response SendMessage(Message MSG)
        {
            try
            {
                var sendingdata = MessageHandle.PrepareDataforSending(MSG,_Encoding);
                _ClientSocket.Send(sendingdata);
                return new Response
                {
                    code = ResponseStatus.Sent,
                    text = ""
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Error In SendMessage Method : " + e.Message);
                return new Response
                {
                    code = ResponseStatus.CanNotSend,
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
                    var message = MessageHandle.PrepareReceivedData(_readbuffer,_Encoding);
                    ClientId = message.FromId;
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

   
       
       

        #endregion
    }
}