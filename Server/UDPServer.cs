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

        public UDPServer()
        {
            ServerPort = 80085;
            _packetBufferSize = 512;

            // Setup our socket for use
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _endPoint = new IPEndPoint(IPAddress.Any, ServerPort);

            // Bind and configure the socket so we are always given the client end point packet information, such as their IP, when data is received.
            _socket.Bind(_endPoint);
            isRunning = true;

            //while (isRunning)
            //    Listen(_socket);
        }

        public void Listen(Socket socket)
        {
            // The BeginReceiveFrom requires us to give it an endpoint, even though we don't use it.
            PacketState state = new PacketState(socket, _packetBufferSize) { Destination = (EndPoint)new IPEndPoint(IPAddress.Any, ServerPort) };
            byte[] buffer = state.Buffer;
            EndPoint destination = state.Destination;

            socket.BeginReceiveFrom(
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
            PacketState state = (PacketState)ar.AsyncState;

            Socket socket = state.UdpSocket;
            EndPoint endPoint = state.Destination;

            //IClientDatagramHeader header = new ClientHeader();

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

                // Acknowledge that we received the packet if it is required.
                if (header.Policy.HasFlag(DatagramPolicy.AcknowledgementRequired) ||
                    _serverPolicy.HasFlag(ServerSpecification.RequireAcknowledgement))
                {
                    ServerMessage.SendClient(new Acknowledge(), endPoint);
                }

                IClientDatagram datagram = CreateDatagramFromClientHeader(header);
                if (datagram == null)
                {
                    // TODO: handle null
                }

                datagram.Deserialize(reader);
            }
        }
    }
}
