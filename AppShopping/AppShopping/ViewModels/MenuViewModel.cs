using AppShopping.Libary.Helpers.MVVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace AppShopping.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public ICommand OpenMapCommand { get; set; }

        public MenuViewModel()
        {
            OpenMapCommand = new Command(OpenMap);
        }

        private async void OpenMap()
        {
            var location = new Location(-22.34222004895834, -49.04938253140704);
            var options = new MapLaunchOptions { Name = "App - Shopping", NavigationMode = NavigationMode.Default };

            try
            {
                await Map.OpenAsync(location, options);
            }catch(Exception e)
            {
                await Shell.Current.DisplayAlert("Erro", "Não conseguimos abrir o mapa do seu celular", "OK");
            }
        }
    }
}
