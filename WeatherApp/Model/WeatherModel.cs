using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using WeatherService;

namespace WeatherApp
{
    public class WeatherModel
    {
        public class ForecastDay
        {
            public class HourData
            {
                public string time { get; set; }
                public string temp_c { get; set; }
                public string temp_f { get; set; }
                public string condition { get; set; }
            }

            public class DayData
            {
                public string date { get; set; }
                public string sunrise { get; set; }
                public string sunset { get; set; }
                public Condition condition { get; set; }
            }

            public DayData day { get; set; }
            public List<HourData> hour { get; set; }
        }

        public class Forecast
        {
            public List<Forecastday> forecastday { get; set; }
        }

        public class Location
        {
            public string name { get; set; }
            public string region { get; set; }
            public string country { get; set; }
            public string localtime { get; set; }
        }

        public class Condition
        {
            public string text { get; set; }
            public string icon { get; set; }
        }

        public class ApiResponse
        {
            public Forecast forecast { get; set; }
        }

        public static ApiResponse FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ApiResponse>(json);
        }
    }
}