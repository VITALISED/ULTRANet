using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Server
{
    internal class UDPServer
    {
        int ServerPort;
        bool isRunning;
        private Socket _socket;
        private IPEndPoint _endPoint;

        public UDPServer()
        {
            ServerPort = 80085;

            // Setup our socket for use
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _endPoint = new IPEndPoint(IPAddress.Any, ServerPort);

            // Bind and configure the socket so we are always given the client end point packet information, such as their IP, when data is received.
            _socket.Bind(_endPoint);
            isRunning = true;

            while (isRunning)
                Listen(_socket);
        }

        public void OnTick()
        {

        }

        public void Listen(Socket socket)
        {
            // The BeginReceiveFrom requires us to give it an endpoint, even though we don't use it.
            var state = new PacketState(socket, PacketBufferSize) { Destination = (EndPoint)new IPEndPoint(IPAddress.Any, ServerPort) };
            byte[] buffer = state.Buffer;
            EndPoint destination = state.Destination;

            socket.BeginReceiveFrom(
                state.Buffer,
                0,
                PacketBufferSize,
                SocketFlags.None,
                ref destination,
                new AsyncCallback(ReceiveClientData),
                state);
        }

        public void ReceiveClientData(IAsyncResult ar)
        {

        }
    }
}
