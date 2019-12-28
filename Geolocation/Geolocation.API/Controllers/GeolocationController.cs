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
        private readonly ILocationValidator locationValidator;

        public GeolocationController(IGeolocationDetailsManager service, ILocationValidator locationValidator)
        {
            this.service = service;
            this.locationValidator = locationValidator;
        }

        [Route(""), HttpPost]
        public IHttpActionResult Post([FromBody] string ipOrUrl)
        {
            if (string.IsNullOrWhiteSpace(ipOrUrl))
            {
                return BadRequest("IP or URL should be provided.");
            }

            GeolocationDetails newItem = null;

            if (locationValidator.IsValidIpAddress(ipOrUrl))
            {
                newItem = service.CreateWithIp(ipOrUrl);
            }
            else if (locationValidator.IsValidUrl(ipOrUrl))
            {
                newItem = service.CreateWithUrl(ipOrUrl);
            }

            if (newItem == null)
            {
                return BadRequest("Invalid IP or URL provided.");
            }

            return Created($"api/geolocation", newItem);
        }
    }
}
