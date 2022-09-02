using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UltraNet.Shared.Network;

namespace Server
{
    internal class ServerMessage
    {
        private List<SVPlayer> _players;
        private int _playerCount;
        private Socket _socket;

        public ServerMessage()
        {
            _players = new List<SVPlayer>();
            _playerCount = 0;
        }

        public void SendAllClients(IServerDatagram datagram)
        {
            foreach(SVPlayer player in _players)
            {
                SendClient(datagram, player.Endpoint);
            }
        }

        public void SendClient(IServerDatagram datagram, EndPoint endpoint)
        {
            //if (!datagram.isValid())
            //    return;

            IServerDatagramHeader header = new ServerHeader(
                1,
                datagram.Identifier,
                DateTime.Now,
                DatagramPolicy.None,
                1
                );

            datagram.Header = header;

            MemoryStream memoryStream = new MemoryStream();
            byte[] data = null;
            using (var binaryWriter = new BinaryWriter(memoryStream))
            {
                datagram.Serialize(binaryWriter);

                data = memoryStream.GetBuffer();

                _socket.SendTo(data, endpoint);
            }
        }

        public void ManageClientMessage(IClientDatagram datagram, EndPoint endpoint)
        {
            switch(datagram.Header.MessageType)
            {
                case (byte)DatagramIdentifiers.Hello:
                    _players.Add(new SVPlayer());
                    //SendAllClients()
                    break;
                default:
                    break;
            }
        }
    }
}
