using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UltraNet.Shared;
using UltraNet.Shared.Network;
using UltraNet.Shared.Network.Datagrams;

namespace Client
{
    public class ClientMessage
    {
        private Socket _socket;
        private IPEndPoint _endpoint;
        private int _clientID;

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
        { 
            switch(datagram.Header.MessageType)
            {
                case (byte)DatagramIdentifiers.Acknowledge:
                    Acknowledge dg = (Acknowledge)datagram;
                    Logger.Msg(dg.ClientID + " new ID!");
                    _clientID = dg.ClientID;
                    break;
                default:
                    break;
            }    
        }
    }
}
