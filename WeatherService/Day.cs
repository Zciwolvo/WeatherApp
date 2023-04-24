using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService
{
    public class Day
    {
        public double avgtemp_c { get; set; }
        public double avgtemp_f { get; set; }
        public double totalprecip_mm { get; set; }
        public double totalprecip_in { get; set; }
        public double totalsnow_cm { get; set; }
        public double avgvis_km { get; set; }
        public double avgvis_miles { get; set; }
        public double avghumidity { get; set; }
        public int daily_chance_of_rain { get; set; }
        public int daily_chance_of_snow { get; set; }
        public Condition condition { get; set; }
    }
}
