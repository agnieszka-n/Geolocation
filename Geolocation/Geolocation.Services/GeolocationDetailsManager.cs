using Geolocation.ControllerModels;
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
        private readonly ILogger logger;
        private readonly IIpStackConfiguration ipStackConfiguration;

        public GeolocationDetailsManager(GeolocationContext context, ILogger logger, IIpStackConfiguration ipStackConfiguration)
        {
            db = context;
            this.logger = logger;
            this.ipStackConfiguration = ipStackConfiguration;
        }

        public GetGeolocationDetailsByIpReturnModel GetByIp(string ip)
        {
            try
            {
                var details = db.GeolocationDetails.SingleOrDefault(x => x.IP == ip);
                return details == null ? null : new GetGeolocationDetailsByIpReturnModel(details);
            }
            catch (Exception ex)
            {
                logger.LogError(this, ex);
                throw;
            }
        }

        public GetGeolocationDetailsByUrlReturnModel GetByUrl(string url)
        {
            try
            {
                var details = db.GeolocationDetails.SingleOrDefault(x => x.URL == url);
                return details == null ? null : new GetGeolocationDetailsByUrlReturnModel(details);
            }
            catch (Exception ex)
            {
                logger.LogError(this, ex);
                throw;
            }
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

            return CreateDetails(newItem);
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

            return CreateDetails(newItem);
        }

        private GeolocationDetails CreateDetails(GeolocationDetails newItem)
        {
            try
            {
                string accessKey = ipStackConfiguration.GetAccessKey();

                if (string.IsNullOrWhiteSpace(accessKey))
                {
                    throw new ApplicationException("Missing IP Stack access key setting.");
                }

                db.GeolocationDetails.Add(newItem);
                db.SaveChanges();

                return newItem;
            }
            catch (Exception ex)
            {
                logger.LogError(this, ex);
                throw;
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
