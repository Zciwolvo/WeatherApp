using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherApp.ViewModel
{
    public class CustomButtonModel : INotifyPropertyChanged
    {
        public string ImageUrl { get; set; }
        public string LabelContent { get; set; }
        public int Tag { get; set; }
        public ICommand ClickCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
