using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace WeatherApp.ViewModel
{
    public class CustomButton : Button
    {
        public static readonly DependencyProperty BitmapProperty =
            DependencyProperty.Register("Bitmap", typeof(BitmapImage), typeof(CustomButton));

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(CustomButton));

        public static readonly DependencyProperty TagProperty =
            DependencyProperty.Register("Tag", typeof(int), typeof(CustomButton));

        public static readonly DependencyProperty CommandProperty =
    DependencyProperty.Register("Command", typeof(ICommand), typeof(CustomButton), new PropertyMetadata(null));



        public BitmapImage Bitmap
        {
            get { return (BitmapImage)GetValue(BitmapProperty); }
            set { SetValue(BitmapProperty, value); }
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public int Tag
        {
            get { return (int)GetValue(TagProperty); }
            set { SetValue(TagProperty, value); }
        }        

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        private void CustomButton_Click(object sender, RoutedEventArgs e)
        {
            // Raise a custom click event for your button
            RoutedEventArgs args = new RoutedEventArgs(Button.ClickEvent, this);
            RaiseEvent(args);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (Command != null && Command.CanExecute(Tag))
            {
                Command.Execute(Tag);
            }
        }
    }
    public class CustomContentControl : ContentControl
    {
        static CustomContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomContentControl), new FrameworkPropertyMetadata(typeof(CustomContentControl)));
        }
    }

}


