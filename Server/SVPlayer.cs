using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Server
{
    internal class SVPlayer
    {
        public EndPoint Endpoint;
        public byte UserID;
        Quaternion viewAngles;
        float PosX;
        float PosY;
        float PosZ;
    }
}
