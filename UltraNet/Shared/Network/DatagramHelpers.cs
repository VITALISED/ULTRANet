using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UltraNet.Shared.Network.Datagrams;

namespace UltraNet.Shared.Network
{
    public class DatagramHelpers
    {
        public IServerDatagram CreateDatagramFromServerHeader(IServerDatagramHeader header)
        {
            IServerDatagram datagram;
            switch(header.MessageType)
            {
                case (byte)DatagramIdentifiers.Acknowledge:
                    datagram = (IServerDatagram)Activator.CreateInstance(typeof(Acknowledge));
                    break;
                default:
                    return null;
            }

            datagram.Header = header;

            return datagram;
        }

        public IClientDatagram CreateDatagramFromClientHeader(IClientDatagramHeader header)
        {
            IClientDatagram datagram;
            switch (header.MessageType)
            {
                case (byte)DatagramIdentifiers.Hello:
                    datagram = (IClientDatagram)Activator.CreateInstance(typeof(Hello));
                    break;
                default:
                    return null;
            }

            datagram.Header = header;

            return datagram;
        }
    }
}
