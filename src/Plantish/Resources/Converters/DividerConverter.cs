using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plantish.Resources.Converters
{
    public class DividerConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Divisor == 0)
            {
                throw new Exception("Can not divide by 0");
            }

            if (value is int valueInput)
            {
                if (valueInput > 0)
                {
                    return valueInput / Divisor;
                }

                return 0;
            }

            return 0;
        }

        public int Divisor { get; set; }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}