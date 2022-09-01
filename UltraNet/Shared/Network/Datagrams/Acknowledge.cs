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
        public IServerDatagramHeader Header { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public byte Identifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
