using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraNet.Shared.Network
{
    public interface IServerDatagramHeader : IDatagramHeader
    {
        byte AppVersion { get; }
        byte MessageType { get; }
    }
}
