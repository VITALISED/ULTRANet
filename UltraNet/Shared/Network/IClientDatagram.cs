using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraNet.Shared.Network
{
    public interface IClientDatagram : IDatagram
    {
        IClientDatagramHeader Header { get; set; }
    }
}
