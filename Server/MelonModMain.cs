using GameConsole;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Console;

namespace Server
{
    public class MelonModMain : MelonMod
    {
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            try
            {
                MonoSingleton<GameConsole.Console>.Instance.RegisterCommands(new ICommand[1]
                {
               new Host()
                });
            } catch (Exception e)
            {
                // lazy but works 
            }
        }
    }
}
