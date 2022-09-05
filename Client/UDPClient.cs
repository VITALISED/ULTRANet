using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UltraNet.Shared.Network;
using UltraNet.Shared.Network.Datagrams;

namespace Client
{
    public class UDPClient
    {
        public ClientMessage ClientMessage;
        private Socket _socket;
        private int _port;
        private DatagramHelpers _datagramHelpers;
        public bool Connected;
        private int _packetBufferSize;
        public IPEndPoint _endPoint;
        public IPEndPoint _localEndpoint;

        public UDPClient(IPEndPoint ipEndpoint, int port)
        {
            _datagramHelpers = new DatagramHelpers();
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            ClientMessage = new ClientMessage(_socket, ipEndpoint);
            _port = port;
            _packetBufferSize = 512;
            Connected = false;
            _endPoint = ipEndpoint;
            _localEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
        }

        public void ConnectToServer()
        {
            _socket.Bind(_localEndpoint);

            Connected = true;

            IClientDatagramHeader header = new ClientHeader(
                1,
                (byte)DatagramIdentifiers.Hello,
                DateTime.Now,
                DatagramPolicy.AcknowledgementRequired,
                1);

            Hello datagram = new Hello();
            datagram.Header = header;
            datagram.Identifier = (byte)DatagramIdentifiers.Hello;
            datagram.Local = true;
            ClientMessage.SendServer(datagram);
        }
        public void Listen()
        {
            // The BeginReceiveFrom requires us to give it an endpoint, even though we don't use it.
            PacketState state = new PacketState(_socket, _packetBufferSize) { Destination = _endPoint };
            byte[] buffer = state.Buffer;
            EndPoint destination = state.Destination;

            if (!Connected)
                return;

            _socket.BeginReceiveFrom(
                state.Buffer,
                0,
                _packetBufferSize,
                SocketFlags.None,
                ref destination,
                new AsyncCallback(ResolveServerData),
                state);

        }

        public void ResolveServerData(IAsyncResult ar)
        {
            PacketState state = (PacketState)ar.AsyncState;

            Socket socket = state.UdpSocket;
            EndPoint endPoint = state.Destination;

            MelonLoader.MelonLogger.Msg("Got Client Packet!!!");

            //IClientDatagramHeader header = new ClientHeader();

            using (var reader = new BinaryReader(new MemoryStream(state.Buffer)))
            {
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                // Read the header in from the buffer first so we know what kind of message and how to route.
                ServerHeader header = new ServerHeader();
                header.Deserialize(reader);
                if (!header.IsValid())
                {
                    throw new InvalidDataException("The header being returned was malformed.");
                }
                MelonLoader.MelonLogger.Msg("appver thing: " + header.AppVersion);
                MelonLoader.MelonLogger.Msg("header thing: " + header.MessageType);

                IServerDatagram datagram = _datagramHelpers.CreateDatagramFromServerHeader(header);
                if (datagram == null)
                {
                    // TODO: handle null
                }

                datagram.Deserialize(reader);

                ClientMessage.ManageServerMessage(datagram);
            }
        }
    }
}
