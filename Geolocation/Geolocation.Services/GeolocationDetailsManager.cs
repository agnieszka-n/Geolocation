using Geolocation.Model;
using Geolocation.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocation.Services
{
    public class GeolocationDetailsManager : IGeolocationDetailsManager, IDisposable
    {
        private GeolocationContext db = new GeolocationContext();

        public GeolocationDetails Create(string ipOrUrl)
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

            return newItem;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
