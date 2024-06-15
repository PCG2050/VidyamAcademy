using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace VidyamAcademy.Converters
{
    public class VideoOverlayImageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is bool isFree && values[1] is string sasToken)
            {
                if (isFree)
                {
                    return "playbutton.png";
                }
                else
                {
                    return string.IsNullOrEmpty(sasToken) ? "padlock.png" : "playbutton.png";
                }
            }
            return "padlock.png";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
