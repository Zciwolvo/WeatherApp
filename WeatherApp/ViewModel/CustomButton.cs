using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WeatherApp.ViewModel
{
    public class CustomButton : ContentControl
    {
        public BitmapImage Bitmap { get; set; }
        public string Label { get; set; }
        public int Tag { get; set; }


    }
}
