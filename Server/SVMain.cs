using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

namespace Server
{
    public class SVMain : MonoBehaviour
    {
        private UDPServer _server;
        bool _started;

        public void Guh()
        {
            _started = true;
            MelonLoader.MelonLogger.Msg("Creating Server...");
            _server = new UDPServer();
        }

        public void Update()
        {
            if(_started)
            {
                _server.Listen();
            }
        }
    }
}
