using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGJ002
{
    class Localization
    {
        public static Localization Instance;
        public enum Language {
            english,
            polish,
            zhongwen,
            max
        };
        public static void Initialize()
        {
            Instance = new Localization();
        }
    }
}
