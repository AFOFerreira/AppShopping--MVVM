using AppShopping.Libary.Helpers.MVVM;
using AppShopping.Models;
using AppShopping.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AppShopping.ViewModels
{
    [QueryProperty("establishmentSerialized", "establishmentSerialized")]
    public class EstablismentDetailViewModel : BaseViewModel
    {
        public Establishment Establishment { get; set; }
        public String establishmentSerialized
        {
            set
            {
                Establishment = JsonConvert.DeserializeObject<Establishment>(Uri.UnescapeDataString(value));
                OnPropertyChanged(nameof(Establishment));
            }
        }
        public EstablismentDetailViewModel()
        {

        }
    }
}
