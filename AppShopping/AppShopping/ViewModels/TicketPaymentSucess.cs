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
    class TicketPaymentSucess : BaseViewModel
    {
        private Ticket ticket;
        private String _number;

        private TicketService _ticketService;

        public ICommand OkCommand { get; set; }

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
        public TicketPaymentSucess()
        {
            _ticketService = new TicketService();
           
            OkCommand = new Command(OK);

            int remove = 1;
            Shell.Current.Navigation.RemovePage(Shell.Current.Navigation.NavigationStack[1]);
        }

        private void OK()
        {
            Shell.Current.Navigation.PopToRootAsync();
        }

    }
}
