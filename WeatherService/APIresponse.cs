using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

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
                    var root = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions();
                    options.PropertyNameCaseInsensitive = true;
                    var data = System.Text.Json.JsonSerializer.Deserialize<Forecast>(root, options);
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