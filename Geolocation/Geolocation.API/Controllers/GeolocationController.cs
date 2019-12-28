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
        private readonly IGeolocationDetailsManager detailsManager;
        private readonly ILocationValidator locationValidator;

        public GeolocationController(IGeolocationDetailsManager detailsManager, ILocationValidator locationValidator)
        {
            this.detailsManager = detailsManager;
            this.locationValidator = locationValidator;
        }

        [Route(""), HttpGet]
        public IHttpActionResult Get(string ip = null, string url = null)
        {
            if (ip != null && url != null)
            {
                return BadRequest("Only one of IP and URL should be provided.");
            }

            if (ip == null && url == null)
            {
                return BadRequest("Either IP or URL should be provided.");
            }

            try
            {
                if (ip != null)
                {
                    if (!locationValidator.IsValidIpAddress(ip))
                    {
                        return BadRequest("Invalid IP provided.");
                    }

                    var modelWithIp = detailsManager.GetByIp(ip);

                    if (modelWithIp == null)
                    {
                        return NotFound();
                    }

                    return Ok(modelWithIp);
                }

                if (!locationValidator.IsValidUrl(url))
                {
                    return BadRequest("Invalid URL provided.");
                }

                var modelWithUrl = detailsManager.GetByUrl(url);

                if (modelWithUrl == null)
                {
                    return NotFound();
                }

                return Ok(modelWithUrl);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Route(""), HttpPost]
        public IHttpActionResult Post([FromBody] string ipOrUrl)
        {
            if (string.IsNullOrWhiteSpace(ipOrUrl))
            {
                return BadRequest("IP or URL should be provided.");
            }

            try
            {
                GeolocationDetails newItem = null;

                if (locationValidator.IsValidIpAddress(ipOrUrl))
                {
                    newItem = detailsManager.CreateWithIp(ipOrUrl);
                }
                else if (locationValidator.IsValidUrl(ipOrUrl))
                {
                    newItem = detailsManager.CreateWithUrl(ipOrUrl);
                }

                if (newItem == null)
                {
                    return BadRequest("Invalid IP or URL provided.");
                }

                return Created($"api/geolocation", newItem);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
