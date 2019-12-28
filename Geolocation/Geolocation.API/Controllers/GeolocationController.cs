using Geolocation.Model;
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
        private GeolocationContext db = new GeolocationContext();

        [Route(""), HttpPost]
        public IHttpActionResult Post([FromBody] string ipOrUrl)
        {
            var newItem = new GeolocationDetails()
            {
                IP = ipOrUrl,
                City = $"city {ipOrUrl}",
                CountryName = $"country {ipOrUrl}",
                ZipCode = $"zip {ipOrUrl}",
            };

            db.GeolocationDetails.Add(newItem);
            db.SaveChanges();

            return Created($"api/geolocation", newItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
