using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Geolocation.Model
{
    public class GeolocationDetails
    {
        public int ID { get; set; }
        public string IP { get; set; }
        public string URL { get; set; }
        public string CountryName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}