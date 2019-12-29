using Geolocation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Geolocation.ControllerModels
{
    public class CreateGeolocationDetailsWithIpReturnModel : AbstractCreateGeolocationDetailsReturnModel
    {
        public string IP { get; set; }

        public CreateGeolocationDetailsWithIpReturnModel(GeolocationDetails model) : base(model)
        {
            IP = model.IP;
        }
    }
}