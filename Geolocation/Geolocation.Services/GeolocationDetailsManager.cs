using Geolocation.Model;
using Geolocation.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocation.Services
{
    public class GeolocationDetailsManager : IGeolocationDetailsManager
    {
        private readonly GeolocationContext db;

        public GeolocationDetailsManager(GeolocationContext context)
        {
            db = context;
        }

        public GeolocationDetails CreateWithIp(string ip)
        {
            var newItem = new GeolocationDetails()
            {
                IP = ip,
                City = $"city {ip}",
                CountryName = $"country {ip}",
                ZipCode = $"zip {ip}",
            };

            db.GeolocationDetails.Add(newItem);
            db.SaveChanges();

            return newItem;
        }

        public GeolocationDetails CreateWithUrl(string url)
        {
            var newItem = new GeolocationDetails()
            {
                URL = url,
                City = $"city {url}",
                CountryName = $"country {url}",
                ZipCode = $"zip {url}",
            };

            db.GeolocationDetails.Add(newItem);
            db.SaveChanges();

            return newItem;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
