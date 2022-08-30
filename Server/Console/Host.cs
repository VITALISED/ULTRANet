using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConsole;
using UnityEngine;

namespace Server.Console
{
    public class Host : ICommand
    {
        public string Name => nameof(Host);
        public string Description => "Host a server in the current scene";
        public string Command => "host";

        public void Execute(GameConsole.Console con, string[] args)
        {
            try
            {
                GameObject.Find("Player").AddComponent<SVMain>().Guh();
            }
            catch (Exception e)
            {
                con.PrintLine("Foiled again: " + e);
            }
        }
    }
}
