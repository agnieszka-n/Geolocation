using Geolocation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Geolocation.ControllerModels
{
    public class GetGeolocationDetailsByIpReturnModel : AbstractGetGeolocationDetailsReturnModel
    {
        public string IP { get; set; }

        public GetGeolocationDetailsByIpReturnModel(GeolocationDetails model) : base(model)
        {
            IP = model.IP;
        }
    }
}