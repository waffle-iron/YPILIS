﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{	
	public class BooleanYesNoConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            string result = "Yes";
            if ((bool)value == false)
            {
                result = "No";
            }
            return result;			
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            bool result = true;
            if (value.ToString().ToUpper() == "NO") result = false;
            return result;
		}
	}
}
