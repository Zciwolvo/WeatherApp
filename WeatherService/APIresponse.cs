using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService
{
    public class WeatherService
    {
        public static async Task<Forecast> GetApiResponse(string location, string KEY)
        {

            var apiURL = $"http://api.WeatherAPI.com/v1/forecast.json?key={KEY}&q={location}&days=6";
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiURL);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Forecast>(json);
                    return data;

                }
                else
                {
                    Console.WriteLine($"Failed to get data from API. Status code: {response.StatusCode}");
                    return null;
                }
            }
        }
    }


}