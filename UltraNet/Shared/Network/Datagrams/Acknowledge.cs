using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraNet.Shared.Network.Datagrams
{
    public class Acknowledge : IServerDatagram
    {
        public int MessageIdAcknowledged { get; set; }
        public int ClientID { get; set; }
        public IServerDatagramHeader Header { get; set; }

        private static byte _identifier = (byte)DatagramIdentifiers.Hello;
        public byte Identifier { get { return _identifier; } set { _identifier = value; } }

        public void Serialize(BinaryWriter serializer)
        {
            serializer.Write(this.MessageIdAcknowledged);
        }

        public void Deserialize(BinaryReader deserializer)
        {
            MessageIdAcknowledged = deserializer.ReadInt32();
        }

        public bool IsValid()
        {
            return this.MessageIdAcknowledged > 0;
        }
    }
}
