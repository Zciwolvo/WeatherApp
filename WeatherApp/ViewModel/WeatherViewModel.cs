using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using WeatherService;

namespace WeatherApp
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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

        public BitmapImage[] CustomButtonBitmap;

        public ObservableCollection<Button> DayButtons { get; set; }

        public string CorrectLocation { get; private set; }

        public bool Success = false;


        public async Task InitializeDataAsync()
        {
            string KEY = "180bc412ee754f4ba9c160652233103";
            var data = await WeatherService.WeatherService.GetApiResponse(Location, KEY);

            if (data != null)
            {
                var DayData = data.forecast.forecastday[_currentDay];
                var HourData = DayData.hour[_currentHour];
                var LocationData = data.forecast.location;
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
                tempC = new string[DayData.hour.Count];
                tempF = new string[DayData.hour.Count];
                CustomLabel = new string[DayData.hour.Count];
                CustomButtonBitmap = new BitmapImage[DayData.hour.Count];
                DayButtons = new ObservableCollection<Button>();
                
                for (int i = 0; i <= 2; i++)
                {
                    tempC[i] = (data.forecast.forecastday[i].day.avgtemp_c).ToString() + "°" + " " + "C";
                    tempF[i] = (data.forecast.forecastday[i].day.avgtemp_f).ToString() + "°" + " " + "F";
                    CustomButtonBitmap[i] = new BitmapImage(new Uri("https:" + data.forecast.forecastday[i].day.condition.icon, UriKind.Absolute));
                    CustomLabel[i] = (_currentTime.AddDays(i)).ToString("ddd", new CultureInfo("en-EN")) + " " + (_unit ? data.forecast.forecastday[i].day.avgtemp_c : data.forecast.forecastday[i].day.avgtemp_f) + (_unit ? " C" : " F");

                }

                for (int i = 0; i < 2; i++)
                {
                    Button DayButton = new Button();
                    DayButton.Tag = "DayButton";
                    DayButton.Content = new StackPanel()
                    {
                        Orientation = Orientation.Vertical,
                        Children =
                    {
                        new Image()
                        {
                                    Source = CustomButtonBitmap[i]
                                },
                                new Label()
                                {
                                    Content = CustomLabel[i],
                                    Tag = i,
                                }
                            }
                    };
                    DayButton.Width = 80;
                    DayButton.Height = 100;
                    DayButton.Click += DayButton_Click;
                    DayButtons.Add(DayButton);

                }
                Success = true;

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
            _unit = !_unit;
        }
        public void UpdateLocation(int Day)
        {
            _currentDay = Day;
        }
        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = (Button)sender;
            var clickedStack = (StackPanel)clickedButton.Content;
            var clickedLabel = (Label)clickedStack.Children[1];
            _currentDay = (int)clickedLabel.Tag; 
        }
    }
}