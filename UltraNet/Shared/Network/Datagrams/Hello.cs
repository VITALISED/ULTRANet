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
        public byte Identifier { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public void Serialize(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
