using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraNet.Shared.Network.Datagrams
{
    public class Hello : IClientDatagram
    {
        public bool Local;
        public IClientDatagramHeader Header { get; set; }
        private static byte _identifier = (byte)DatagramIdentifiers.Hello;
        public byte Identifier { get { return _identifier; } set { _identifier = value; } }
        public void Deserialize(BinaryReader reader)
        {
            Local = reader.ReadBoolean();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Local);
        }
    }
}
