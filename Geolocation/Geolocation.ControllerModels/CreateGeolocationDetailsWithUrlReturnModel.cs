using Geolocation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Geolocation.ControllerModels
{
    public class CreateGeolocationDetailsWithUrlReturnModel : AbstractCreateGeolocationDetailsReturnModel
    {
        public string URL { get; set; }

        public CreateGeolocationDetailsWithUrlReturnModel(GeolocationDetails model) : base(model)
        {
            URL = model.URL;
        }
    }
}