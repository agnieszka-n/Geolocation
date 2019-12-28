using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Geolocation.Model
{
    public class GeolocationContextInitializer : CreateDatabaseIfNotExists<GeolocationContext>
    {
        protected override void Seed(GeolocationContext context)
        {
            context.GeolocationDetails.Add(new GeolocationDetails()
            {
                IP = "134.201.250.155",
                CountryName = "United States",
                City = "Los Angeles",
                ZipCode = "90013"
            });
            context.GeolocationDetails.Add(new GeolocationDetails()
            {
                IP = "2001:0:9d38:6ab8:1c48:3a1c:a95a:b1c2",
                CountryName = "United States",
                City = "Santa Monica",
                ZipCode = "90292"
            });
            context.GeolocationDetails.Add(new GeolocationDetails()
            {
                IP = "212.77.98.9",
                CountryName = "Poland",
                City = "Sopot",
                ZipCode = "80-009"
            });
            context.GeolocationDetails.Add(new GeolocationDetails()
            {
                URL = "onet.pl",
                CountryName = "Poland",
                City = "Grudziądz",
                ZipCode = "86-132"
            });

            context.SaveChanges();
        }
    }
}