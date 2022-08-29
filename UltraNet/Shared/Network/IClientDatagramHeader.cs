using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraNet.Shared.Network
{
    public interface IClientDatagramHeader : IDatagramHeader
    {
        byte AppVersion { get; }
        byte MessageType { get; }
        int ClientId { get; }
    }
}
