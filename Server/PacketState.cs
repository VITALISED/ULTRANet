using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class PacketState
    {
        internal PacketState(Socket socket, int bufferSize)
        {
            this.UdpSocket = socket;
            this.Buffer = new byte[bufferSize];
            TimeStamp = DateTime.Now;
        }

        public byte[] Buffer { get; set; }

        public EndPoint Destination { get; set; }

        public Socket UdpSocket { get; }

        public DateTime TimeStamp { get; set; }
    }
}
