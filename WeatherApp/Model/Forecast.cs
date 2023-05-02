using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class Forecast
    {
        public Location location { get; set; }
        public Current current { get; set; }

        public Forecast2 forecast { get; set; }

    }
}
