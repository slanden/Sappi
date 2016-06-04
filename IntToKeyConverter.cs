using System;
using System.Globalization;
using System.Windows.Data;

namespace Sappi
{
    class IntToKeyConverter : IValueConverter
    {
        //since student info is stored as ints (keys to string values), this is needed to convert
        //the numbers to strings for viewing purposes.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == App.formData.status)
                return App.formData.status[(int)value];

            if ((int) value == -1)
                return "(Not Provided)";

            return App.formData.masterList[(int) value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
