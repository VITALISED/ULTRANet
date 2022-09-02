using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UltraNet.Shared.Network;

namespace Client
{
    internal class ClientMessage
    {
        private Socket _socket;
        private IPEndPoint _endpoint;

        public ClientMessage(Socket socket, IPEndPoint endPoint)
        {
            _socket = socket;
            _endpoint = endPoint;
        }

        public void SendServer(IClientDatagram datagram)
        {
            MemoryStream memoryStream = new MemoryStream();
            byte[] data = null;
            using (var binaryWriter = new BinaryWriter(memoryStream))
            {
                datagram.Serialize(binaryWriter);

                data = memoryStream.GetBuffer();

                _socket.SendTo(data, _endpoint);
            }
        }

        public void ManageServerMessage(IServerDatagram datagram)
        { }
    }
}
