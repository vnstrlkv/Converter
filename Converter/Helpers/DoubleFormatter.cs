using System;
using Windows.UI.Xaml.Data;

namespace Converter.Helpers
{
    internal class DoubleFormatter : IValueConverter // вспомогательный класс для конвертации и отслеживания вводимых символов
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                return value.ToString();
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            double n;
            bool isNumeric = double.TryParse(value.ToString(), out n);
            if (isNumeric)
            {
                n = Math.Round(n, 3);
                return Math.Abs(n);
            }
            else
            {
                return (double)0;
            }
        }
    }
}
