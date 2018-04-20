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
        private MySocketType _Sockettype = MySocketType.Socket;
        private bool _SocketHandshake = false; 
        private string WebSocketSecKey { get; set; }
        private Encoding _Encoding { get; }
        public string ClientId { set; get; }
        public MessageUM SendingMessage { set; get; }

        private string ClientIp { get; }
        private readonly byte[] _readbuffer = new byte[10000];
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

        public Response SendMessage(MessageUM MSG)
        {
            try
            {
                var sendingdata = MessageHandle.PrepareDataforSending(MSG,_Encoding,_Sockettype,WebSocketSecKey);
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
                    var data = _Encoding.GetString(_readbuffer);
                    if (new System.Text.RegularExpressions.Regex("websocket").IsMatch(data) && !_SocketHandshake)
                    {
                        WebSocketSecKey = new System.Text.RegularExpressions.Regex("Sec-WebSocket-Key: (.*)")
                            .Match(data).Groups[1].Value.Trim(); 
                        const string eol = "\r\n"; // HTTP/1.1 defines the sequence CR LF as the end-of-line marker

                        Byte[] response = Encoding.UTF8.GetBytes("HTTP/1.1 101 Switching Protocols" + eol
                        + "Connection: Upgrade" +eol+ "Upgrade: websocket" +eol
                        + "Sec-WebSocket-Accept: " +Convert.ToBase64String(System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(
                        WebSocketSecKey
                        +"258EAFA5-E914-47DA-95CA-C5AB0DC85B11"))) + eol+ eol);
                        
                        _ClientSocket.Send(response);
                        _SocketHandshake = true;
                        _Sockettype = MySocketType.Websocket;
                    }
                    else
                    {
                        var message = MessageHandle.PrepareReceivedData(_readbuffer, _Encoding);
                        if (!string.IsNullOrWhiteSpace(message.Text) && !string.IsNullOrWhiteSpace(message.FromId) &&
                            !string.IsNullOrWhiteSpace(message.ToId))
                        {
                            ClientId = string.IsNullOrWhiteSpace(ClientId) ? message.FromId : ClientId;
                            SendingMessageQueue.SendingMessages.Enqueue(message);
                        }
                    }
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

   
       
        private string EncodeBase64(string text)
        {
            if (text == null)
            {
                return null;
            }

            byte[] textAsBytes = _Encoding.GetBytes(text);
            return System.Convert.ToBase64String(textAsBytes);
        }

        #endregion
    }
}