using System;
using System.Globalization;

namespace VidyamAcademy.Converters
{
    public class BoolToOverlayImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Video video)
            {
                if (video.IsFree || !string.IsNullOrEmpty(video.SasToken))
                {
                    return "playbutton.png"; 
                }
                else
                {
                    return "padlock.png"; 
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
