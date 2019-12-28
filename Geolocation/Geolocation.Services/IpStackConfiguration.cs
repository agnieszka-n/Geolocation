using Geolocation.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Geolocation.Services
{
    public class IpStackConfiguration : IIpStackConfiguration
    {
        public string GetAccessKey()
        {
            return WebConfigurationManager.AppSettings["IpStackAccessKey"];
        }
    }
}
