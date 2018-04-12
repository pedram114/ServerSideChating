using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Busines.Classes;
using Business.Interfaces;
using Business.SocketClasses;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApp
{
    public class Program
    {
        private static readonly ITCPListener _listener = new TCPListener(9990);

        public static void Main(string[] args)
        {
            _listener.ListenToPort();
            
            ISendingMessagesHandle messageshandle=new SendingMessagesHandle();
            var nth=new Thread(new ThreadStart(messageshandle.CheckMessageQueue));
            nth.Start();
            
            var nths=new Thread(new ThreadStart(AcceptSocket));
            nths.Start();

           
            BuildWebHost(args).Run();
           


        }

        private static void AcceptSocket()
        {
            while (true)
            {
                _listener.AcceptSocket();
            }
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}