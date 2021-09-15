using AppShopping.Libary.Enums;
using AppShopping.Libary.Helpers.MVVM;
using AppShopping.Models;
using AppShopping.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppShopping.ViewModels
{
    public abstract class EstablishmentViewModel: BaseViewModel
    {
        public string SearchWord { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DetailCommand { get; set; }

        private EstablishmentType _establishmentType;

        private List<Establishment> _establishments;
        public List<Establishment> Establishments
        {
            get
            {
                return _establishments;
            }
            set
            {
                SetProperty(ref _establishments, value);
            }
        }
        private List<Establishment> _allEstablishments;

        public EstablishmentViewModel(EstablishmentType establishmentType)
        {
            _establishmentType = establishmentType;
             SearchCommand = new Command(Search);
            DetailCommand = new Command<Establishment>(Detail);
            var establishments = new EstablishmentService().
                GetEstablishments().Where(x => x.Type == _establishmentType).ToList();

            Establishments = establishments;
            _allEstablishments = establishments;
        }

        private void Detail(Establishment establishment)
        {
            String establishmentSerialized = JsonConvert.SerializeObject(establishment);
            Shell.Current.GoToAsync($"establishment/Detail?establishmentSerialized={Uri.EscapeDataString(establishmentSerialized)}");

        }

        private void Search()
        {
            Establishments = _allEstablishments.Where(a => a.Name.ToLower().Contains(SearchWord.ToLower())).ToList();
        }
    }
}
