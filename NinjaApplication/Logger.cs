using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaApplication
{
    internal class Logger
    {
        private static bool _configured = false;

        internal static ILogger GetLogger()
        {
            if (!_configured)
            {
                var config = new NLog.Config.LoggingConfiguration();
                var logconsole = new NLog.Targets.ConsoleTarget("console");
                logconsole.Layout = "${message}";
                _configured = true;
            }

            return LogManager.GetCurrentClassLogger();
        }
    }
}
