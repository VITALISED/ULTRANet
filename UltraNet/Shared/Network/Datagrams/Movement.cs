using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraNet.Shared.Network.Datagrams
{
    public class Movement : IClientDatagram
    {
        public byte AnimationParameter;

        public float QuaternionX;
        public float QuaternionY;
        public float QuaternionZ;
        public float QuaternionW;

        public float PositionX;
        public float PositionY;
        public float PositionZ;
        public IClientDatagramHeader Header { get; set; }
        private static byte _identifier = (byte)DatagramIdentifiers.Movement;
        public byte Identifier { get { return _identifier; } set { _identifier = value; } }

        public void Serialize(System.IO.BinaryWriter serializer)
        {
            serializer.Write(AnimationParameter);
            serializer.Write(QuaternionX);
            serializer.Write(QuaternionY);
            serializer.Write(QuaternionZ);
            serializer.Write(QuaternionW);
            serializer.Write(PositionX);
            serializer.Write(PositionY);
            serializer.Write(PositionZ);
        }

        public void Deserialize(System.IO.BinaryReader deserializer)
        {
            AnimationParameter = deserializer.ReadByte();
            QuaternionX = deserializer.ReadSingle();
            QuaternionY = deserializer.ReadSingle();
            QuaternionZ = deserializer.ReadSingle();
            QuaternionW = deserializer.ReadSingle();
            PositionX = deserializer.ReadSingle();
            PositionY = deserializer.ReadSingle();
            PositionZ = deserializer.ReadSingle();
        }

        public bool IsValid()
        {
            return true;
        }
    }
}
