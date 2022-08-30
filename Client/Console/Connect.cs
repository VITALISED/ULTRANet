using GameConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Console
{
    public class Connect : ICommand
    {
        public string Name => nameof(Connect);

        public string Description => "Connect to a server (ip:port)";

        public string Command => "connect";

        public void Execute(GameConsole.Console con, string[] args)
        {
            try
            {
                GameObject.Find("Player").AddComponent<CLMain>().Guh(args[0]);
            }
            catch (Exception e)
            {
                con.PrintLine("Foiled again: " + e);
            }
        }
    }
}
