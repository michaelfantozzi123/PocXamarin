﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace PocXamarin.Converters
{
	public class InverseBooleanConverter : IValueConverter
	{
		public InverseBooleanConverter()
		{
		}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

