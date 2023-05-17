using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace WeatherApp.ViewModel
{
    public class CustomContentControl : ContentControl
    {
        static CustomContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomContentControl), new FrameworkPropertyMetadata(typeof(CustomContentControl)));
        }
    }
}
