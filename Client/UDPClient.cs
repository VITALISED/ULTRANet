using System;
using System.Collections.Generic;
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

        public UDPClient(IPEndPoint ipEndpoint, int port)
        {
            _datagramHelpers = new DatagramHelpers();
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _clientMessage = new ClientMessage(_socket, ipEndpoint);
            _port = port;
        }

        public void ConnectToServer(bool listen)
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, _port);
            var localEndPoint = (EndPoint)ipEndPoint;

            var header = new ClientHeader(
                1,
                DatagramIdentifiers.Hello,
                DateTime.Now,
                (DatagramPolicy.SequencedMessage | DatagramPolicy.AcknowledgementRequired),
                1);

            Hello datagram = (Hello)_datagramHelpers.CreateDatagramFromClientHeader(header);

            datagram.Local = false;

            if (listen)
                datagram.Local = true;
           
            _clientMessage.SendServer(datagram);
        }
    }
}
