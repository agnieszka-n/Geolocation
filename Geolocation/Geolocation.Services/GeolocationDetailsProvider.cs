using Geolocation.Services.Interfaces;
using Geolocation.Services.Interfaces.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Geolocation.Services
{
    public class GeolocationDetailsProvider : IGeolocationDetailsProvider
    {
        public async Task<GeolocationDetails> GetAsync(string accessKey, string ipOrUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.ipstack.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"{ipOrUrl}?access_key={accessKey}");

                string content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<GeolocationDetails>(content);
                }

                throw new Exception($"An error occurred while getting geolocation details from external API:\r\n{content}");
            }
        }
    }
}
