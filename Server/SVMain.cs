using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Server
{
    public class SVMain : MonoBehaviour
    {
        private UDPServer _server;
        bool _started;

        public void Guh()
        {
            _started = true;
            _server = new UDPServer();
        }

        public void FixedUpdate()
        {
            if(_started)
            {
                MelonLoader.MelonLogger.Msg("OnTick");
                _server.Listen();
            }
        }
    }
}
