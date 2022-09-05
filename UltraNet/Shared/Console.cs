using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraNet.Shared
{
    public static class Logger
    {
        public static void Msg(object msg)
        {
            MelonLoader.MelonLogger.Msg(msg);
            GameConsole.Console.print(msg);
        }
    }
}
