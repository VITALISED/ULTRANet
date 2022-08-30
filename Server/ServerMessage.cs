using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UltraNet.Shared.Network;

namespace Server
{
    internal class ServerMessage
    {
        public void SendAllClients()
        { }

        public void SendClient(IServerDatagram datagram, EndPoint endpoint)
        { }
    }
}
