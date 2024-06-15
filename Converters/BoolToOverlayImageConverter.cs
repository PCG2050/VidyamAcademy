﻿using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace VidyamAcademy.Converters
{
    public class BoolToOverlayImageConverter : IValueConverter
    {           

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isFree)
            {
                return isFree ? "playbutton" : "padlock.png";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

