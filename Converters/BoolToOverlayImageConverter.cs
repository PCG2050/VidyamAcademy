
using System.Globalization;


namespace VidyamAcademy.Converters
{
    public class BoolToOverlayImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isFree)
            {
                return isFree ? "playbutton.png" : "padlock.png";
            }
            return "padlock.png"; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
