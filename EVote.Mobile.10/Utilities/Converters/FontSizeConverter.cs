using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace EVote.Utilities.Converters
{
    public class FontSizeConverter : IValueConverter
    {
        // https://stackoverflow.com/questions/19436368/trying-to-resize-textbox-font-size-in-text-box-with-c-sharp-and-wpf-can-only
        // RESPONSIVE TEXT THAT ALLOWS FOR OVERFLOWING THE CONTAINER INSTEAD OF SIZE TO FIT
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double actualHeight = System.Convert.ToDouble(value);
            int fontSize = (int)(actualHeight * .5);
            return fontSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FontSizeHalfConverter : IValueConverter
    {
        // https://stackoverflow.com/questions/19436368/trying-to-resize-textbox-font-size-in-text-box-with-c-sharp-and-wpf-can-only
        // RESPONSIVE TEXT THAT ALLOWS FOR OVERFLOWING THE CONTAINER INSTEAD OF SIZE TO FIT
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double actualHeight = System.Convert.ToDouble(value);
            int fontSize = (int)(actualHeight * .5);
            return fontSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FontSizeFullConverter : IValueConverter
    {
        // https://stackoverflow.com/questions/19436368/trying-to-resize-textbox-font-size-in-text-box-with-c-sharp-and-wpf-can-only
        // RESPONSIVE TEXT THAT ALLOWS FOR OVERFLOWING THE CONTAINER INSTEAD OF SIZE TO FIT
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double actualHeight = System.Convert.ToDouble(value);
            int fontSize = (int)(actualHeight * .8);
            return fontSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FontSizeExtraConverter : IValueConverter
    {
        // https://stackoverflow.com/questions/19436368/trying-to-resize-textbox-font-size-in-text-box-with-c-sharp-and-wpf-can-only
        // RESPONSIVE TEXT THAT ALLOWS FOR OVERFLOWING THE CONTAINER INSTEAD OF SIZE TO FIT
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double actualHeight = System.Convert.ToDouble(value);
            int fontSize = (int)((actualHeight) * 0.8);
            return fontSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FontAwesomeSizeConverter : IValueConverter
    {
        // https://stackoverflow.com/questions/19436368/trying-to-resize-textbox-font-size-in-text-box-with-c-sharp-and-wpf-can-only
        // RESPONSIVE TEXT THAT ALLOWS FOR OVERFLOWING THE CONTAINER INSTEAD OF SIZE TO FIT
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double actualHeight = System.Convert.ToDouble(value);
            int fontSize = (int)(actualHeight * 1.05);
            return fontSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FontSmallSizeConverter : IValueConverter
    {
        // https://stackoverflow.com/questions/19436368/trying-to-resize-textbox-font-size-in-text-box-with-c-sharp-and-wpf-can-only
        // RESPONSIVE TEXT THAT ALLOWS FOR OVERFLOWING THE CONTAINER INSTEAD OF SIZE TO FIT
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double actualHeight = System.Convert.ToDouble(value);
            int fontSize = (int)(actualHeight * .5);
            return fontSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
