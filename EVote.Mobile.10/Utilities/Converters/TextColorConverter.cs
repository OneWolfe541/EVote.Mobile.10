using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace EVote.Utilities.Converters
{
    public class TextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal d = System.Convert.ToDecimal(value);
            return d < 5;
        }//

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// Change text color based on Party values
    /// </summary>
    public class PartyColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string d = value.ToString();
                return d == "DEM" || d == "REP" || d == "LIB" ? true : false;
            }
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// Change text color based on Party values
    /// </summary>
    public class PartyVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                bool d = bool.Parse(value.ToString());
                return d == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            }
            else
            {
                return System.Windows.Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// Change text color based on log code values
    /// </summary>
    public class BallotStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            else return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class HighlightButtonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values != null && values[0] != null && values[1] != null)
            {
                var result = values[0].ToString() == values[1].ToString();
                if(result == true)
                {
                    var test = result;
                }
                return !result;
            }
            else
            {
                return true;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            string[] values = null;
            if (value != null)
                return values = value.ToString().Split(' ');
            return values;
        }
    }
}
