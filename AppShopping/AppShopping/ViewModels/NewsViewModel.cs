using AppShopping.Libary.Helpers.MVVM;
using AppShopping.Models;
using AppShopping.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppShopping.ViewModels
{
    public class NewsViewModel :BaseViewModel
    {
        private NewsServices _newsService { get; set; }

        public List<News> News { get; set; }

    
        public NewsViewModel()
        {
            _newsService = new NewsServices();
            News = _newsService.GetNews();
        }

    }
}
