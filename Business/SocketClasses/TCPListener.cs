
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using Business.Interfaces;
using Business.SocketClasses;

namespace Busines.Classes
{
    public class TCPListener : ITCPListener
    {
        #region Properties
        private readonly TcpListener _listener;
        public static IList<ClientSocket> Allclients;
        #endregion


        #region CTOR
        public TCPListener(int portnumber)
        {
            if (_listener == null)
            {
                _listener = new TcpListener(IPAddress.Any, portnumber);
            }
            else
            {
                _listener.Server.Bind(new IPEndPoint(IPAddress.Any, portnumber));
            }

            if (Allclients == null) {
                Allclients = new List<ClientSocket>();
            }
        }
        #endregion



        public void ListenToPort()
        {            
            _listener.Start();
            Console.WriteLine("Server Started");
        }

        public void AcceptSocket()
        {
            _listener.BeginAcceptSocket(new AsyncCallback(AcceptCallback),
                _listener);
        }

        private static void AcceptCallback(IAsyncResult arr) {
          
            // Get the socket that handles the client request.
            var sockets = (System.Net.Sockets.TcpListener) arr.AsyncState;
            var handler = sockets.EndAcceptSocket(arr);

            var newclient = new ClientSocket(handler,Encoding.ASCII);
            newclient.WaitforData();
            Allclients.Add(newclient);         
        }

        private static void ssss(IAsyncResult ssss)
        {
            
        }

    }
}
