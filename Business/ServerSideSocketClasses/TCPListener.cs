using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Business.Interfaces;
using DataAccessLayer;

namespace Business.ServerSideSocketClasses
{
    public class TCPListener : ITCPListener
    {
        
        
        #region Properties

        private readonly PedramDbContext _context;
        private readonly TcpListener _listener;
        public static IList<ClientSocket> Allclients;

        #endregion
        
        #region CTOR

        public TCPListener(int portnumber,PedramDbContext context)
        {
            if (_listener == null)
                _listener = new TcpListener(IPAddress.Any, portnumber);
            else
                _listener.Server.Bind(new IPEndPoint(IPAddress.Any, portnumber));
            _context = context;
            if (Allclients == null) Allclients = new List<ClientSocket>();
            ListenToPort();
        }

        #endregion


        private void ListenToPort()
        {
            _listener.Start();
            Console.WriteLine("Server Started");
        }

        public void AcceptSocket()
        {
            _listener.BeginAcceptSocket(AcceptCallback,
                _listener);
        }

        private void AcceptCallback(IAsyncResult arr)
        {
            // Get the socket that handles the client request.
            var sockets = (TcpListener) arr.AsyncState;
            var handler = sockets.EndAcceptSocket(arr);
            _listener.BeginAcceptSocket(AcceptCallback,
                _listener);
            var newclient = new ClientSocket(handler, Encoding.UTF8);
            
            var nths = new Thread(CheckDatarezeive);
            nths.Start(newclient);
 
            
            Allclients.Add(newclient);
        }

        private static void CheckDatarezeive(object client)
        {
//            while (true)
//            {
                ((ClientSocket)client).WaitforData();
           //     Thread.Sleep(2000);
          //  }
            
        }


    }
}