using System;
using System.Globalization;
using System.Windows.Data;
using System.Diagnostics;

namespace Sappi
{
    class ContentToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if(value != null)
            //    return (value as System.Windows.Controls.Image).Source;
            Debugger.Break();
            
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //throw new NotImplementedException();
            Debugger.Break();
            return value;
        }
    }
}
