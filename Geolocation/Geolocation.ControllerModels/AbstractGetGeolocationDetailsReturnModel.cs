using Geolocation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Geolocation.ControllerModels
{
    public abstract class AbstractGetGeolocationDetailsReturnModel
    {
        public string CountryName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        protected AbstractGetGeolocationDetailsReturnModel(GeolocationDetails model)
        {
            City = model.City;
            CountryName = model.CountryName;
            ZipCode = model.ZipCode;
        }
    }
}