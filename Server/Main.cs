using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Server
{
    public class Main : MonoBehaviour
    {
        private UDPServer _server;

        public void Awake()
        {
            _server = new UDPServer();
        }

        public void FixedUpdate()
        {
            _server.OnTick();
        }
    }
}
