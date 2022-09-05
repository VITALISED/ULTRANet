using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Server
{
    public struct SVPlayer
    {
        public EndPoint Endpoint;
        public int UserID;

        public SVPlayer(int userID, EndPoint endPoint)
        {
            UserID = userID; Endpoint = endPoint;
        }
    }
}
