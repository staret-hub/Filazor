using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filazor.Core.Shared
{
    public class Common
    {
        public static string USER_FILE_PATH = "./Data/User.json";

        public static void DebugPrint(string message)
        {
#if DEBUG
            Console.WriteLine("{0} >>> {1}", DateTime.Now.ToString("HH:mm:ss.fff"), message);
#endif
        }
    }
}
