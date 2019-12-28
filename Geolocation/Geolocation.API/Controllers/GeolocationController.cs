using Geolocation.Model;
using Geolocation.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Geolocation.API.Controllers
{
    [RoutePrefix("api/geolocation")]
    public class GeolocationController : ApiController
    {
        private readonly IGeolocationDetailsManager service;

        public GeolocationController(IGeolocationDetailsManager service)
        {
            this.service = service;
        }

        [Route(""), HttpPost]
        public IHttpActionResult Post([FromBody] string ipOrUrl)
        {
            GeolocationDetails newItem = service.Create(ipOrUrl);
            return Created($"api/geolocation", newItem);
        }
    }
}
