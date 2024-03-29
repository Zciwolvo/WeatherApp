﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace WeatherApp.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private string _location;
        private int _currentDay = 0;
        private DateTime _currentTime = DateTime.Now;
        private int _currentHour = DateTime.Now.Hour;
        private bool _unit = true;


        public event PropertyChangedEventHandler PropertyChanged;

        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged(nameof(_location));
            }
        }

        public int CurrentHour
        {
            get { return _currentHour; }
        }

        public bool Unit
        {
            get { return _unit; }
            set
            {
                _unit = value;
                OnPropertyChanged(nameof(_unit));
            }
        }



        public string[] CustomLabel { get; private set; }

        public string[] tempC { get; private set; }

        public string[] tempF { get; private set; }

        public string StatusLabel { get; private set; }

        public string Temp { get; private set; }

        public string TempSign { get; private set; }

        public string TempFeelsLike { get; private set; }

        public string TempFeelsLikeSign { get; private set; }

        public string Sunrise { get; private set; }

        public string Sunset { get; private set; }

        public string Humidity { get; private set; }

        public string Wind { get; private set; }

        public string WindDir { get; private set; }

        public string Pressure { get; private set; }

        public string Visibility { get; private set; }

        public string Cloud { get; private set; }

        public string RainChance { get; private set; }

        public string SnowChance { get; private set; }

        public string Precip { get; private set; }

        public string Date { get; private set; }

        public BitmapImage bitmap = new BitmapImage();

        public string[] CustomButtonBitmap { get; set; }
        public int[] Tags { get; set; } 
        public int DaysLen { get; set; }
        public bool Success = false;

        public ObservableCollection<CustomButtonModel> DayButtons { get; set; }
        private static async Task<Forecast> GetApiResponse(string location, string KEY)
        {

            var apiURL = $"http://api.WeatherAPI.com/v1/forecast.json?key={KEY}&q={location}&days=3";
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiURL);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions();
                    options.PropertyNameCaseInsensitive = true;
                    var data = System.Text.Json.JsonSerializer.Deserialize<Forecast>(json, options);
                    return data;

                }
                else
                {
                    Console.WriteLine($"Failed to get data from API. Status code: {response.StatusCode}");
                    return null;
                }
            }
        }
        public async Task InitializeDataAsync()
        {
            string KEY = "180bc412ee754f4ba9c160652233103";
            var data = await GetApiResponse(Location, KEY);

            if (data != null)
            {
                var DayData = data.forecast.forecastday[_currentDay];
                var HourData = DayData.hour[_currentHour];
                var LocationData = data.location;
                Location = LocationData.name;
                StatusLabel = DayData.day.condition.text;
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("https:" + DayData.day.condition.icon, UriKind.Absolute);
                bitmap.EndInit();
                Temp = Unit ? (HourData.temp_c + "°") : (HourData.temp_f + "°");
                TempSign = Unit ? "C" : "F";
                TempFeelsLike = Unit ? (HourData.feelslike_c + "°") : (HourData.feelslike_f + "°");
                TempFeelsLikeSign = Unit ? "C" : "F";
                Sunrise = DateTime.Parse(DayData.astro.sunrise).ToString("h:mm tt", CultureInfo.InvariantCulture);
                Sunset = DateTime.Parse(DayData.astro.sunset).ToString("h:mm tt", CultureInfo.InvariantCulture);
                Humidity = HourData.humidity + "%";
                Wind = Unit ? $"{HourData.wind_kph} km/h" : $"{HourData.wind_mph} mph";
                WindDir = HourData.wind_dir;
                Pressure = Unit ? $"{HourData.pressure_mb} mb" : $"{HourData.pressure_in} in";
                Visibility = Unit ? $"{HourData.vis_km} km" : $"{HourData.vis_miles} mi";
                Cloud = HourData.cloud + "%";
                RainChance = HourData.chance_of_rain + "%";
                SnowChance = HourData.chance_of_snow + "%";
                Precip = Unit ? $"{HourData.precip_mm} mm" : $"{HourData.precip_in} in";
                Date = DateTime.Parse(DayData.date).ToString("dddd, dd MMMM yyyy", CultureInfo.InvariantCulture);
                DaysLen = data.forecast.forecastday.Count;
                DayButtons = new ObservableCollection<CustomButtonModel>();
                tempC = new string[DaysLen];
                tempF = new string[DaysLen];
                CustomLabel = new string[DaysLen];
                CustomButtonBitmap = new string[DaysLen];
                Tags = new int[DaysLen];
               

                for (int i = 0; i < DaysLen; i++)
                {
                    tempC[i] = (data.forecast.forecastday[i].day.avgtemp_c).ToString() + "°" + " " + "C";
                    tempF[i] = (data.forecast.forecastday[i].day.avgtemp_f).ToString() + "°" + " " + "F";
                    CustomLabel[i] = (_currentTime.AddDays(i)).ToString("ddd", new CultureInfo("en-EN")) + " " + (Unit ? tempC[i] : tempF[i]);
                    CustomButtonBitmap[i] = "https:" + data.forecast.forecastday[i].day.condition.icon;
                    Tags[i] = i;
                }
                Success = true;
                OnPropertyChanged(nameof(DayButtons));
            }
            else Success = false;
        }



        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void UpdateCurrentHour(int Hour)
        {
            _currentHour = Hour;
        }
        public void UpdateUnit()
        {
            Unit = !_unit;
            OnPropertyChanged(nameof(Unit));
        }
        public void UpdateLocation(int Day)
        {
            _currentDay = Day;
        }

        public void UpdateCurrentDay(int Day)
        {
            _currentDay = Day;
        }

    }
}