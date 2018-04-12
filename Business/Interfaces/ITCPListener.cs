using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ITCPListener
    {
        void ListenToPort() ;

        void AcceptSocket();
    }
}
