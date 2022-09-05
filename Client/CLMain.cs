using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;
using Object = UnityEngine.Object;

namespace Client
{
    public class CLMain : MonoBehaviour
    {
        public UDPClient Client;

        public void Guh(string formattedEndpoint)
        {
            string[] subs = formattedEndpoint.Split(':');
            
            IPAddress ip = IPAddress.Parse(subs[0]);

            int port = int.Parse(subs[1]);

            IPEndPoint ipEndpoint = new IPEndPoint(ip, port);

            Client = new UDPClient(ipEndpoint, port);

            Client.ConnectToServer();

        }

        public void Update()
        {

            Client.Listen();
        }
    }
}
