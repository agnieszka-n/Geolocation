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
        private readonly IGeolocationDetailsProvider geolocationDetailsProvider;

        public GeolocationDetailsManager(GeolocationContext context, ILogger logger, IIpStackConfiguration ipStackConfiguration, IGeolocationDetailsProvider geolocationDetailsProvider)
        {
            db = context;
            this.logger = logger;
            this.ipStackConfiguration = ipStackConfiguration;
            this.geolocationDetailsProvider = geolocationDetailsProvider;
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

        public async Task<CreateGeolocationDetailsWithIpReturnModel> CreateWithIpAsync(string ip)
        {
            var existingDetails = db.GeolocationDetails.SingleOrDefault(x => x.IP == ip);

            if (existingDetails != null)
            {
                return new CreateGeolocationDetailsWithIpReturnModel(existingDetails);
            }

            var newItem = new GeolocationDetails()
            {
                IP = ip
            };

            GeolocationDetails model = await CreateDetailsAsync(ip, newItem);
            return new CreateGeolocationDetailsWithIpReturnModel(model);
        }

        public async Task<CreateGeolocationDetailsWithUrlReturnModel> CreateWithUrlAsync(string url)
        {
            var existingDetails = db.GeolocationDetails.SingleOrDefault(x => x.URL == url);

            if (existingDetails != null)
            {
                return new CreateGeolocationDetailsWithUrlReturnModel(existingDetails);
            }

            var newItem = new GeolocationDetails()
            {
                URL = url
            };

            GeolocationDetails model = await CreateDetailsAsync(url, newItem);
            return new CreateGeolocationDetailsWithUrlReturnModel(model);
        }

        private async Task<GeolocationDetails> CreateDetailsAsync(string ipOrUrl, GeolocationDetails newItem)
        {
            try
            {
                string accessKey = ipStackConfiguration.GetAccessKey();

                if (string.IsNullOrWhiteSpace(accessKey))
                {
                    throw new ApplicationException("Missing IP Stack access key setting.");
                }

                Interfaces.Data.GeolocationDetails details = await geolocationDetailsProvider.GetAsync(accessKey, ipOrUrl);

                if (details.Success == false)
                {
                    HandleUnsuccessfulDetailsRequest(details);
                }

                newItem.City = details.City;
                newItem.CountryName = details.CountryName;
                newItem.ZipCode = details.ZipCode;

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

        private void HandleUnsuccessfulDetailsRequest(Interfaces.Data.GeolocationDetails details)
        {
            if (details.Error.Type == "invalid_access_key")
            {
                throw new Exception("Invalid access key.");
            }
            else
            {
                throw new Exception(details.Error.Info);
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
