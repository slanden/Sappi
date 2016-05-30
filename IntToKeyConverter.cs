using System;
using System.Globalization;
using System.Windows.Data;

namespace Sappi
{
    class IntToKeyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == App.formData.status)
                return App.formData.status[(int)value];

            return App.formData.masterList[(int) value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
