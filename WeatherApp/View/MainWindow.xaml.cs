using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using static WeatherApp.ViewModel.WeatherViewModel;

namespace WeatherApp
{
    public partial class MainWindow : Window
    {
        private readonly ViewModel.WeatherViewModel viewModel;
        private bool _enabled = false;
        public ICommand DayButtonClickCommand{ get; set; }  

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel.WeatherViewModel();
            DayButtonClickCommand = new RelayCommand<object>(async delegate (object parameter)
            {
                int day = (int)parameter;
                viewModel.UpdateCurrentDay(day);
                await viewModel.InitializeDataAsync();
                EnableLabels();
            });
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Location = locationInput.Text;
            if (viewModel.Location != "")
            { 
                await viewModel.InitializeDataAsync();
                _enabled = true;
                if (viewModel.Success) EnableLabels();
            }

        }

        private async void UnitButton_Click(object sender, RoutedEventArgs e)
        {

            viewModel.UpdateUnit();
            await viewModel.InitializeDataAsync();
            if (_enabled) EnableLabels();
        }

        private async void Handle_Slider(object sender, RoutedEventArgs e)
        {
            var slider = (Slider)sender;
            viewModel.UpdateCurrentHour((int)slider.Value);
            await viewModel.InitializeDataAsync();
            EnableLabels();
        }

        private void EnableLabels()
        {
            locationLabel.Content = viewModel.Location;
            Temp.Content = viewModel.Temp;
            TempSign.Content = viewModel.TempSign;
            StatusLabel.Content = viewModel.StatusLabel;
            Date.Content = viewModel.Date + " " + viewModel.CurrentHour + ":" + "00";
            TempFeelsLike.Content = viewModel.TempFeelsLike;
            TempFeelsLikeSign.Content = viewModel.TempFeelsLikeSign;
            StatusLabel.Content = viewModel.StatusLabel;
            statusIcon.Source = viewModel.bitmap;
            DataGrid.Visibility = System.Windows.Visibility.Visible;
            Sunrise.Content = "Sunrise: " + viewModel.Sunrise;
            Sunset.Content = "Sunset: " + viewModel.Sunset;
            Humidity.Content = "Humidity: " + viewModel.Humidity; 
            Wind.Content = "Wind: " + viewModel.Wind + " " + viewModel.WindDir;
            Pressure.Content = "Pressure: " + viewModel.Pressure;
            Visibility.Content = "Visibility: " + viewModel.Visibility;
            Cloud.Content = "Cloud: " + viewModel.Cloud;
            RainChance.Content = "RainChance: " + viewModel.RainChance;
            SnowChance.Content = "SnowChance: " + viewModel.SnowChance;
            Precip.Content = "Precip: " + viewModel.Precip;
            HourSlider.Visibility = System.Windows.Visibility.Visible;
            HourSlider.Value = viewModel.CurrentHour;
            for (int i = 0; i < viewModel.DaysLen; i++)
            {
                ViewModel.CustomButtonModel buttonModel = new ViewModel.CustomButtonModel()
                {
                    ImageUrl = viewModel.CustomButtonBitmap[i],
                    LabelContent = viewModel.CustomLabel[i],
                    Tag = viewModel.Tags[i],
                    ClickCommand = DayButtonClickCommand,
                };

                viewModel.DayButtons.Add(buttonModel);
            }
            DataContext = viewModel;
        }
    }
}