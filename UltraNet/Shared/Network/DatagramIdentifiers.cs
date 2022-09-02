using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraNet.Shared.Network
{
    public enum DatagramIdentifiers
    {
        Hello = 0,
        Movement = 1,
        Heartbeat = 2,
        Acknowledge = 3,
    }
}
