using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Client
{
    public class CLMain : MonoBehaviour
    {
        private UDPClient _client;

        public void Guh(string formattedEndpoint)
        {
            string[] subs = formattedEndpoint.Split(':');
            IPAddress ip = IPAddress.Parse(subs[0]);
            int port = int.Parse(subs[1]);
            IPEndPoint ipEndpoint = new IPEndPoint(ip, port);

            _client = new UDPClient(ipEndpoint, port);
        }
    }
}
