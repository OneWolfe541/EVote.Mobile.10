using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace EVote.Utilities.Converters
{
    public class ListViewSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double actualHeight = System.Convert.ToDouble(value);
            //int itemSize = (int)(actualHeight * 1.15);
            int itemSize = 65;
            return itemSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ListViewHalfSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double actualHeight = System.Convert.ToDouble(value);
            //int itemSize = (int)(actualHeight * 1.15);
            int itemSize = 32;
            return itemSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ListViewWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double actualWidth = System.Convert.ToDouble(value);
            int itemSize = (int)(actualWidth * 0.965);
            return itemSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
