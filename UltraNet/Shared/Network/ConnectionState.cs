using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraNet.Shared.Network
{
    public enum ConnectionState
    {
        Authenticating = 0,
        Loading = 1,
        Connected = 2,
        Playing = 3,
        Unused1 = 4,
        Unused2 = 5
    }
}
