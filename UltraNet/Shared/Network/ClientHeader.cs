using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraNet.Shared.Network;

namespace UltraNet.Shared.Network
{
    public class ClientHeader : IClientDatagramHeader
    {
        public int ClientId { get; set; }
        public byte AppVersion { get; private set; }
        public bool IsLastInSequence { get; private set; }
        public byte MessageType { get; private set; }
        public long TransmissionTime { get; private set; }
        public byte Channel { get; private set; }
        public DatagramPolicy Policy { get; private set; }
        public byte SequenceNumber { get; private set; }
    
        public ClientHeader()
        { }

        public ClientHeader(
            byte appVersion,
            byte messageType,
            DateTime transmission,
            DatagramPolicy policy,
            byte sequenceNumber)
        {
            AppVersion = appVersion;
            MessageType = messageType;
            TransmissionTime = transmission.Ticks;
            Policy = policy;
            SequenceNumber = sequenceNumber;
            TransmissionTime = transmission.Ticks;
        }

        public void Serialize(BinaryWriter serializer)
        {
            serializer.Write(AppVersion);
            serializer.Write(MessageType);
            serializer.Write(IsLastInSequence);
            serializer.Write(TransmissionTime);
            serializer.Write(Channel);
            serializer.Write((byte)Policy);

            if (Policy.HasFlag(DatagramPolicy.SequencedMessage))
            {
                serializer.Write(SequenceNumber);
            }
        }

        public void Deserialize(BinaryReader deserializer)
        {
            AppVersion = deserializer.ReadByte();
            MessageType = deserializer.ReadByte();
            IsLastInSequence = deserializer.ReadBoolean();
            TransmissionTime = deserializer.ReadInt64();
            Channel = deserializer.ReadByte();
            Policy = (DatagramPolicy)deserializer.ReadByte();

            if (Policy.HasFlag(DatagramPolicy.SequencedMessage))
            {
                SequenceNumber = deserializer.ReadByte();
            }
        }

        public bool IsValid()
        {
            return true;
        }
    }
}
