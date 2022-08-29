using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UltraNet.Shared.Buffers
{
    public struct UserConnectionBuffer
    {
        public byte ConnectionState;
        public uint UserID;
        public char[] Auth; //unused
    }
}
