using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraNet.Shared.Network
{
    public class ClientHeader : IClientDatagramHeader
    {
        public ClientHeader()
        {
        }

        public ClientHeader(
            byte appVersion,
            byte messageType,
            DateTime transmission,
            DatagramPolicy policy,
            byte sequenceNumber)
        {
            this.AppVersion = appVersion;
            this.MessageType = messageType;
            this.TransmissionTime = transmission.Ticks;
            this.Policy = policy;
            this.SequenceNumber = sequenceNumber;
        }

        public int ClientId { get; set; }

        public byte AppVersion { get; private set; }

        public bool IsLastInSequence { get; private set; }

        public byte MessageType { get; private set; }

        public long TransmissionTime { get; private set; }

        public byte Channel { get; private set; }

        public DatagramPolicy Policy { get; private set; }

        public byte SequenceNumber { get; private set; }

        public void Serialize(BinaryWriter serializer)
        {
            serializer.Write(this.AppVersion);
            serializer.Write(this.MessageType);
            serializer.Write(this.IsLastInSequence);
            serializer.Write(this.TransmissionTime);
            serializer.Write(this.Channel);
            serializer.Write((byte)this.Policy);

            if (this.Policy.HasFlag(DatagramPolicy.SequencedMessage))
            {
                serializer.Write(this.SequenceNumber);
            }
        }

        public void Deserialize(BinaryReader deserializer)
        {
            this.AppVersion = deserializer.ReadByte();
            this.MessageType = deserializer.ReadByte();
            this.IsLastInSequence = deserializer.ReadBoolean();
            this.TransmissionTime = deserializer.ReadInt64();
            this.Channel = deserializer.ReadByte();
            this.Policy = (DatagramPolicy)deserializer.ReadByte();

            if (this.Policy.HasFlag(DatagramPolicy.SequencedMessage))
            {
                this.SequenceNumber = deserializer.ReadByte();
            }
        }

        public bool IsValid()
        {
            return true;
        }
    }
}
