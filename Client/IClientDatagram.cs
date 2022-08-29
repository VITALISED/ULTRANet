using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraNet.Shared.Network;

namespace Client
{
    public interface IClientDatagram : IDatagram
    {
        byte Version { get; }
        NetMessageType MessageType { get; }
        uint ClientID { get; }
    }
}
