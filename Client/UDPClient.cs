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
        private Socket _socket;
        private ClientMessage _clientMessage;
        private int _port;
        private DatagramHelpers _datagramHelpers;
        private bool _connected;
        private int _packetBufferSize;

        public UDPClient(IPEndPoint ipEndpoint, int port)
        {
            _datagramHelpers = new DatagramHelpers();
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _clientMessage = new ClientMessage(_socket, ipEndpoint);
            _port = port;
            _packetBufferSize = 512;
            _connected = false;
        }

        public void ConnectToServer()
        {
            IClientDatagramHeader header = new ClientHeader(
                1,
                (byte)DatagramIdentifiers.Hello,
                DateTime.Now,
                (DatagramPolicy.SequencedMessage | DatagramPolicy.AcknowledgementRequired),
                1);

            Hello datagram = new Hello();
            datagram.Header = header;
            datagram.Identifier = (byte)DatagramIdentifiers.Hello;
            datagram.Local = true;

            _clientMessage.SendServer(datagram);
        }
        public void Listen()
        {
            // The BeginReceiveFrom requires us to give it an endpoint, even though we don't use it.
            PacketState state = new PacketState(_socket, _packetBufferSize) { Destination = (EndPoint)new IPEndPoint(IPAddress.Any, _port) };
            byte[] buffer = state.Buffer;
            EndPoint destination = state.Destination;

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

                IServerDatagram datagram = _datagramHelpers.CreateDatagramFromServerHeader(header);
                if (datagram == null)
                {
                    // TODO: handle null
                }

                datagram.Deserialize(reader);

                _clientMessage.ManageServerMessage(datagram);
            }
        }
    }
}
