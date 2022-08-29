using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraNet.Shared.Network
{
    public enum NetMessageType
    {
        Connecting = 0,
        Ack = 1,
        Position = 2,
        Cosmetics = 3,
        Animation = 4,
    }
}
