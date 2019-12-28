﻿using Geolocation.Model;
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
        private GeolocationContext db = new GeolocationContext();

        public GeolocationController(IGeolocationDetailsManager service)
        {
            this.service = service;
        }

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