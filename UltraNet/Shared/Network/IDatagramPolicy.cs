using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraNet.Shared.Network
{
    [Flags]
    public enum DatagramPolicy : byte
    {
        None = 0,
        SequencedMessage = 1,
        AcknowledgementRequired = 2,
        CompletedSequenceRequired = 4,
    }
}
