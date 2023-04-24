using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI
{
    public class Forecast
    {
        public List<Forecastday> forecastday { get; set; }

        public Location location { get; set; }
        public Current current { get; set; }

        public Forecast forecast { get; set; }
    }
}
