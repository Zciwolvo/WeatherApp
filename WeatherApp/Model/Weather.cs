using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherService;

namespace WeatherApp.Model
{
    public class Weather : INotifyPropertyChanged
    {
        public string Location;
        public Task<Forecast> CurrentWeather { 
            get{ return CurrentWeather; } 
            set 
            {
                string KEY = "180bc412ee754f4ba9c160652233103";
                CurrentWeather = WeatherService.WeatherService.GetApiResponse(KEY, Location); 
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
