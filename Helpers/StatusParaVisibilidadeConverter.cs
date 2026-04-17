using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfApp.Helpers
{
    public class StatusParaVisibilidadeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var statusAtual = value as string;
            var statusNecessario = parameter as string;
            return statusAtual == statusNecessario ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
