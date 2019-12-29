using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocation.Services.Interfaces
{
    public interface IGeolocationDetailsProvider
    {
        Task<Data.GeolocationDetails> GetAsync(string accessKey, string ipOrUrl);
    }
}
