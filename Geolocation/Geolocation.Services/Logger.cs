using Geolocation.Services.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocation.Services
{
    public class Logger : Interfaces.ILogger
    {
        public void LogError(object source, Exception ex)
        {
            NLog.Logger logger = LogManager.GetLogger(source.GetType().FullName);
            logger.Error(ex);
            Debug.WriteLine(ex);
        }
    }
}
