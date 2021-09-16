using AppShopping.Libary.Helpers.MVVM;
using AppShopping.Models;
using AppShopping.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppShopping.ViewModels
{
    [QueryProperty("Number", "number")]
    public class TicketPaymentViewModel : BaseViewModel
    {
        private String _number;
        private Ticket _ticket;

        public Ticket Ticket
        {
            get { return _ticket; }
            set
            {
                SetProperty(ref _ticket, value);
            }
        }

        public String Number
        {
            set
            {
                _number = value;
                SetProperty(ref _number, value);
                Ticket = _ticketService.GetTicketInfo(value);
                //TODO -Atribuir data final e calcular o valor do ticket
            }
        }

        private CreditCard _creditCard;

        public CreditCard CreditCard
        {
            get { return _creditCard; }
            set { SetProperty(ref _creditCard, value); }
        }


        private TicketService _ticketService;
        public TicketPaymentViewModel()
        {
            _ticketService = new TicketService();
        }
    }
}
