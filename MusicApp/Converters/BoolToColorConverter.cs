using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Converters
{
    public class BoolToColorConverter : IValueConverter

    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)

        {

            if (value is bool isSelected && parameter is string colors)

            {

                var colorStrings = colors.Split(',');

                return isSelected ? Color.FromArgb(colorStrings[0]) : Color.FromArgb(colorStrings[1]);

            }

            return Colors.White;

        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)

        {

            throw new NotImplementedException();

        }

    }
}
