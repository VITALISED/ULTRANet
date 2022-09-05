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
    [Flags]
    public enum ServerSpecification
    {
        None = 0,
        RequireAcknowledgement = 1,
    }

    internal class UDPServer
    {
        int ServerPort;
        bool isRunning;
        private Socket _socket;
        private IPEndPoint _endPoint;
        private int _packetBufferSize;
        private ServerSpecification _serverPolicy;
        private ServerMessage _serverMessage;
        private DatagramHelpers _datagramHelpers;
        public List<SVPlayer> ServerPlayers;

        public UDPServer()
        {
            ServerPort = 25569;
            _packetBufferSize = 512;

            // Setup our socket for use
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _endPoint = new IPEndPoint(IPAddress.Any, ServerPort);

            // Bind and configure the socket so we are always given the client end point packet information, such as their IP, when data is received.
            _socket.Bind(_endPoint);

            _serverMessage = new ServerMessage(this);
            _serverPolicy = ServerSpecification.None;
            _datagramHelpers = new DatagramHelpers();

            isRunning = true;
        }

        public void Listen()
        {
            // The BeginReceiveFrom requires us to give it an endpoint, even though we don't use it.
            PacketState state = new PacketState(_socket, _packetBufferSize) { Destination = (EndPoint)new IPEndPoint(IPAddress.Any, ServerPort) };
            byte[] buffer = state.Buffer;
            EndPoint destination = state.Destination;

            _socket.BeginReceiveFrom(
                state.Buffer,
                0,
                _packetBufferSize,
                SocketFlags.None,
                ref destination,
                new AsyncCallback(ReceiveClientData),
                state);
        }

        public void ReceiveClientData(IAsyncResult ar)
        {
            Logger.Msg("Received client packet");
            PacketState state = (PacketState)ar.AsyncState;

            Socket socket = state.UdpSocket;
            EndPoint endPoint = state.Destination;

            using (var reader = new BinaryReader(new MemoryStream(state.Buffer)))
            {
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                // Read the header in from the buffer first so we know what kind of message and how to route.
                ClientHeader header = new ClientHeader();
                header.Deserialize(reader);
                if (!header.IsValid())
                {
                    throw new InvalidDataException("The header being returned was malformed.");
                }

                Logger.Msg(header.AppVersion + " appver");
                Logger.Msg(header.Policy + " policy");
                Logger.Msg(header.MessageType + " type");

                // Acknowledge that we received the packet if it is required.
                //if (header.Policy.HasFlag(DatagramPolicy.AcknowledgementRequired) ||
                //    _serverPolicy.HasFlag(ServerSpecification.RequireAcknowledgement))
                //{
                //    Acknowledge ack = new Acknowledge(); // change this to a different dgram later
                //    ack.MessageIdAcknowledged = 1;

                //    if (header.ClientId == 0)
                //    {
                //        SVPlayer newPlayer = new SVPlayer(new Random().Next(1, 10000000), endPoint);
                //        ack.ClientID = newPlayer.UserID;
                //        ServerPlayers.Add(newPlayer);
                //        _serverMessage.SendClient(ack, endPoint);
                //    }
                //    else
                //    {
                //        foreach (SVPlayer player in ServerPlayers)
                //        {
                //            if(header.ClientId == player.UserID)
                //            {
                //                ack.ClientID = header.ClientId;
                //            }
                //        }
                //    }

                //}
                IClientDatagram datagram = _datagramHelpers.CreateDatagramFromClientHeader(header);
                if (datagram == null)
                {
                    Logger.Msg("Null datagram :/");
                    // TODO: handle null
                }
                datagram.Deserialize(reader);
                _serverMessage.ManageClientMessage(datagram, endPoint);

            }
        }
    }
}
