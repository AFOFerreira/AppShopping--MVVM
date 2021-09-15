using AppShopping.Libary.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AppShopping.Libary.Converter
{
    public class EstableshmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EstablishmentType establishment = (EstablishmentType)value;
            return (establishment == EstablishmentType.Store) ? "Loja" : "Restaurante";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
