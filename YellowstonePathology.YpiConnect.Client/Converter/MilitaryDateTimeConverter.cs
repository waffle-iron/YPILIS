﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.YpiConnect.Client.Converter
{
	[ValueConversion(typeof(DateTime), typeof(String))]
	public class MilitaryDateTimeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
			{
				return string.Empty;
			}
			DateTime dateTime = (DateTime)value;
			if(dateTime.ToShortTimeString() == "12:00 AM")
			{
                return dateTime.ToString("MM/dd/yyy");
			}
			return dateTime.ToString("MM/dd/yyy HH:mm");
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string strValue = value.ToString();
			if (strValue == string.Empty)
			{
				return null;
			}
			else
			{
				string fmtString = strValue;
				DateTime dt;
				if (strValue.IndexOf("/") == -1)
				{
					if (strValue.IndexOf(" ") == 8)
					{
						fmtString = fmtString.Insert(2, "/");
						fmtString = fmtString.Insert(5, "/");
					}
				}

				DateTime.TryParse(fmtString, out dt);
				return dt;
			}
		}
	}
}
