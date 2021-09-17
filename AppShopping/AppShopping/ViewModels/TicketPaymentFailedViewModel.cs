using AppShopping.Libary.Helpers.MVVM;
using AppShopping.Models;
using AppShopping.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppShopping.ViewModels
{
    [QueryProperty("Number", "number")]
    [QueryProperty("Message", "message")]
    class TicketPaymentFailedViewModel : BaseViewModel
    {

        private Ticket ticket;
        private String _number;

        private TicketService _ticketService;

        public ICommand OkCommand { get; set; }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }


        public Ticket Ticket
        {
            get { return ticket; }
            set { SetProperty(ref ticket, value); }
        }
        public String Number
        {
            set
            {
                _number = value;
                SetProperty(ref _number, value);
                Ticket = _ticketService.GetTicket(value);
                //TODO -Atribuir data final e calcular o valor do ticket
            }
        }
        public TicketPaymentFailedViewModel()
        {
            _ticketService = new TicketService();
            OkCommand = new Command(OK);
        }
       
        private void OK()
        {
            Shell.Current.Navigation.PopAsync();
        }

    }
}
