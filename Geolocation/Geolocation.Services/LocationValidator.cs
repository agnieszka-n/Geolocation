using Geolocation.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocation.Services
{
    public class LocationValidator : ILocationValidator
    {
        public bool IsValidIpAddress(string text)
        {
            var hostName = Uri.CheckHostName(text);
            return hostName == UriHostNameType.IPv4 || hostName == UriHostNameType.IPv6;
        }

        public bool IsValidUrl(string text)
        {
            return Uri.CheckHostName(text) == UriHostNameType.Dns;
        }
    }
}
