using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraNet.Shared.Network.Datagrams
{
    public class WorldPlayerMove : IServerDatagram
    {
        public int ClientID;
        public byte AnimationParameter;

        public float QuaternionX;
        public float QuaternionY;
        public float QuaternionZ;
        public float QuaternionW;

        public float PositionX;
        public float PositionY;
        public float PositionZ;

        public IServerDatagramHeader Header { get; set; }
        private static byte _identifier = (byte)DatagramIdentifiers.Movement;
        public byte Identifier { get { return _identifier; } set { _identifier = value; } }
        public void Deserialize(BinaryReader reader)
        {
            ClientID = reader.ReadInt32();
            AnimationParameter = reader.ReadByte();
            QuaternionX = reader.ReadSingle();
            QuaternionY = reader.ReadSingle();
            QuaternionZ = reader.ReadSingle();
            QuaternionW = reader.ReadSingle();
            PositionX = reader.ReadSingle();
            PositionY = reader.ReadSingle();
            PositionZ = reader.ReadSingle();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(ClientID);
            writer.Write(AnimationParameter);
            writer.Write(QuaternionX);
            writer.Write(QuaternionY);
            writer.Write(QuaternionZ);
            writer.Write(QuaternionW);
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(PositionZ);
        }
    }
}
