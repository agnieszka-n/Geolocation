using Geolocation.ControllerModels;
using Geolocation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocation.Services.Interfaces
{
    public interface IGeolocationDetailsManager
    {
        GetGeolocationDetailsByIpReturnModel GetByIp(string ip);
        GetGeolocationDetailsByUrlReturnModel GetByUrl(string url);

        Task<CreateGeolocationDetailsWithIpReturnModel> CreateWithIpAsync(string ip);
        Task<CreateGeolocationDetailsWithUrlReturnModel> CreateWithUrlAsync(string url);
        
        void DeleteByIp(string ip);
        void DeleteByUrl(string url);
    }
}
