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

namespace Server
{
    internal class ServerMessage
    {
        private Socket _socket;
        private UDPServer Server;

        public ServerMessage(UDPServer server)
        {
            Server = server;
        }

        public void SendAllClients(byte messageType, IServerDatagram datagram)
        {
            foreach(SVPlayer player in Server.ServerPlayers)
            {
                SendClient(messageType, datagram, player.Endpoint);
            }
        }

        public void SendClient(byte messageType, IServerDatagram datagram, EndPoint endpoint)
        {
            Logger.Msg("fucker");

            IServerDatagramHeader header = new ServerHeader(
                1,
                3,
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
            Logger.Msg("Hit ManageClientMessage");
            switch(datagram.Header.MessageType)
            {
                case (byte)DatagramIdentifiers.Hello:
                    Logger.Msg("Hello!");
                    Acknowledge ack = new Acknowledge(); // change this to a different dgram later
                    ack.MessageIdAcknowledged = 1;
                    Logger.Msg("Hello 2!");

                    int newPlayerID = new Random().Next(1, 100000);
                    Logger.Msg("Hello 3!");

                    //Server.ServerPlayers.Add(new SVPlayer(newPlayerID, endpoint));
                    Logger.Msg("Hello 4!");

                    ack.ClientID = newPlayerID;
                    Logger.Msg("Hello 5!");
                    SendClient(3, ack, endpoint);
                    Logger.Msg("Hello 6!");
                    break;
            default:
                break;
            }
        }
    }
}
