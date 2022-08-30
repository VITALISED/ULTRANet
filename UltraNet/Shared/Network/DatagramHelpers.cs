using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UltraNet.Shared.Network
{
    public class DatagramHelpers
    {
        private static readonly Dictionary<byte, Type> _datagrams = new Dictionary<byte, Type>();

        public DatagramHelpers()
        {
            Type datagramInterfaceType = typeof(IDatagram);
            IEnumerable<Type> datagramTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes().Where(type => datagramInterfaceType.IsAssignableFrom(type)));

            cacheDatagramTypes(datagramTypes);
        }

        public DatagramHelpers(IEnumerable<Type> datagramTypes)
        {
            cacheDatagramTypes(datagramTypes);
        }

        private void cacheDatagramTypes(IEnumerable<Type> datagramTypes)
        {
            foreach (Type datagram in datagramTypes)
            {
                if (datagram.IsAbstract || !datagram.IsClass)
                {
                    continue;
                }

                IDatagram propertyInfo = (IDatagram)datagram.GetInterface("IDatagram");
                _datagrams.Add(propertyInfo.Identifier, datagram);
            }
        }

        public IServerDatagram CreateDatagramFromServerHeader(IServerDatagramHeader header)
        {
            Type datagramType = null;
            if (!_datagrams.TryGetValue(header.MessageType, out datagramType))
            {
                return null;
            }

            IServerDatagram datagram = (IServerDatagram)Activator.CreateInstance(datagramType);
            datagram.Header = header;

            return datagram;
        }

        public IClientDatagram CreateDatagramFromClientHeader(IClientDatagramHeader header)
        {
            Type datagramType = null;
            if (!_datagrams.TryGetValue(header.MessageType, out datagramType))
            {
                return null;
            }

            IClientDatagram datagram = (IClientDatagram)Activator.CreateInstance(datagramType);
            datagram.Header = header;

            return datagram;
        }
    }
}
