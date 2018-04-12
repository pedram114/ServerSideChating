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
        public static void Main(string[] args)
        {
            ITCPListener listener = new TCPListener(9990);
            listener.ListenToPort();
            
            ISendingMessagesHandle messageshandle=new SendingMessagesHandle();
            var nth=new Thread(new ThreadStart(messageshandle.CheckMessageQueue));
            nth.Start();
            
            while (true)
            {
                listener.AcceptSocket();
            }
            BuildWebHost(args).Run();
           


        }

       

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}