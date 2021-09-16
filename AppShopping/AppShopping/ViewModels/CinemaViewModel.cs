
using AppShopping.Libary.Helpers.MVVM;
using AppShopping.Models;
using AppShopping.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppShopping.ViewModels
{
    public class CinemaViewModel : BaseViewModel
    {
        public ObservableCollection<Film> Films { get; set; }
        public ICommand FilmDetailComand { get; set; }

        public CinemaViewModel()
        {
            FilmDetailComand = new Command<Film>(FilmDetail);

            var lista = new ObservableCollection<Film>(new CinemaService().GetFilmes());
            Films = lista;
        }   
        
        private void FilmDetail(Film film)
        {
            //TODO - Serializar filme e enviar via URI
           var filmSerialized =  JsonConvert.SerializeObject(film);

            Shell.Current.GoToAsync($"film/detail?filmSerialized={Uri.EscapeDataString(filmSerialized)}");
        }
    }
}
