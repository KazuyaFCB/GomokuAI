using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace _1712912
{
    class RelativeToAbsolutePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string relative = (string)value;
            if (relative[relative.Length - 1] == 'g')
                return AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + relative;
            return AppDomain.CurrentDomain.BaseDirectory + "Engine\\" + relative;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
