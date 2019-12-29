using Geolocation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocation.ControllerModels
{
    public class AbstractCreateGeolocationDetailsReturnModel
    {
        public string CountryName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        protected AbstractCreateGeolocationDetailsReturnModel(GeolocationDetails model)
        {
            City = model.City;
            CountryName = model.CountryName;
            ZipCode = model.ZipCode;
        }
    }
}
