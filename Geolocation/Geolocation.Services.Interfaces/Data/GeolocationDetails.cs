using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocation.Services.Interfaces.Data
{
    public class GeolocationDetails
    {
        public bool? Success { get; set; }
        public GeolocationDetailsError Error { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }
        public string City { get; set; }
        [JsonProperty("zip")]
        public string ZipCode { get; set; }
    }
}
