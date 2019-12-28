using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Geolocation.Model
{
    public class GeolocationContext: DbContext
    {
        public GeolocationContext() : base("GeolocationContext")
        {
            Database.SetInitializer(new GeolocationContextInitializer());
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<GeolocationDetails> GeolocationDetails { get; set; }
    }
}