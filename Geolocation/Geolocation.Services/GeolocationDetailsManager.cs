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

        public CreateGeolocationDetailsWithIpReturnModel CreateWithIp(string ip)
        {
            var newItem = new GeolocationDetails()
            {
                IP = ip,
                City = $"city {ip}",
                CountryName = $"country {ip}",
                ZipCode = $"zip {ip}",
            };

            GeolocationDetails model = CreateDetails(newItem);
            return new CreateGeolocationDetailsWithIpReturnModel(model);
        }

        public CreateGeolocationDetailsWithUrlReturnModel CreateWithUrl(string url)
        {
            var newItem = new GeolocationDetails()
            {
                URL = url,
                City = $"city {url}",
                CountryName = $"country {url}",
                ZipCode = $"zip {url}",
            };

            GeolocationDetails model = CreateDetails(newItem);
            return new CreateGeolocationDetailsWithUrlReturnModel(model);
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

        public void DeleteByIp(string ip)
        {
            try
            {
                var itemToDelete = db.GeolocationDetails.SingleOrDefault(x => x.IP == ip);

                if (itemToDelete != null)
                {
                    db.GeolocationDetails.Remove(itemToDelete);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(this, ex);
                throw;
            }
        }

        public void DeleteByUrl(string url)
        {
            try
            {
                var itemToDelete = db.GeolocationDetails.SingleOrDefault(x => x.URL == url);

                if (itemToDelete != null)
                {
                    db.GeolocationDetails.Remove(itemToDelete);
                    db.SaveChanges();
                }
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
