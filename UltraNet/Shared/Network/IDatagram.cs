using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraNet.Shared.Network
{
    public interface IDatagram
    {
        byte Identifier { get; set; }

        void Serialize(BinaryWriter writer);
        void Deserialize(BinaryReader reader);
    }
}
