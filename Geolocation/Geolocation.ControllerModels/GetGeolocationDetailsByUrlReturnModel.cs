using Geolocation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Geolocation.ControllerModels
{
    public class GetGeolocationDetailsByUrlReturnModel : AbstractGetGeolocationDetailsReturnModel
    {
        public string URL { get; set; }

        public GetGeolocationDetailsByUrlReturnModel(GeolocationDetails model) : base(model)
        {
            URL = model.URL;
        }
    }
}