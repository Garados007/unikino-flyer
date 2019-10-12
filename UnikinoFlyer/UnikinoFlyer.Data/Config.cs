using System;
using System.Collections.Generic;
using System.Text;
using MaxLib.Data.IniFiles;

namespace UnikinoFlyer.Data
{
    public static class Config
    {
        static OptionsLoader options;

        static Config()
        {
            options = new OptionsLoader("config.ini", false);
        }

        public static OptionsKey GetRoot(string key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            return options?[0].Options.FindName(key);
        }
    }
}
