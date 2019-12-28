using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Geolocation.API
{
    internal static class LoggerInitalizer
    {
        public static void Initialize()
        {
            var config = new LoggingConfiguration();

            var fileTarget = new FileTarget("fileTarget")
            {
                Encoding = Encoding.UTF8,
                FileName = "Geolocation.log",
                Layout = "${longdate} ${level:uppercase=true}\t${logger}\n${message}\n"
            };

            config.AddTarget(fileTarget);
            config.AddRuleForAllLevels(fileTarget);
            LogManager.Configuration = config;
        }
    }
}