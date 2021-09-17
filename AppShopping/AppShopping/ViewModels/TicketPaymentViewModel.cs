using AppShopping.Libary.Helpers.MVVM;
using AppShopping.Libraries.Validators;
using AppShopping.Models;
using AppShopping.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppShopping.ViewModels
{
    [QueryProperty("Number", "number")]
    public class TicketPaymentViewModel : BaseViewModel
    {
        private String _number;
        private Ticket _ticket;
        private string _messages;

        public string Messages
        {
            get { return _messages; }
            set { SetProperty(ref _messages, value); }
        }


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
                Ticket = _ticketService.GetTicketToPaid(value);
            }
        }

        private CreditCard _creditCard;

        public CreditCard CreditCard
        {
            get { return _creditCard; }
            set { SetProperty(ref _creditCard, value); }
        }

        public ICommand PaymentCommand { get; set; }

        private TicketService _ticketService;

        private PaymentService _paymentService;
        public TicketPaymentViewModel()
        {
            _ticketService = new TicketService();
            PaymentCommand = new MvvmHelpers.Commands.AsyncCommand(Payment);
            CreditCard = new CreditCard();
            _paymentService = new PaymentService();
        }

        private async Task Payment()
        {
            //TODO - VALIDAR - Manual, Data anotations e fluent validation.
            try
            {
                Messages = string.Empty;
                string message = Valid(CreditCard);

                if (string.IsNullOrEmpty(message))
                {
                    string transactonId = _paymentService.SendPayment(CreditCard,Ticket);
                    Ticket.transactionID = transactonId;
                    Ticket.Status = Libary.Enums.TicketStatus.paid;

                    _ticketService.UpdateTicket(Ticket);

                    await Shell.Current.GoToAsync($"ticket/payment/success?number={Ticket.Number}");
                }
                else
                {
                    Messages = message;
                }
            }
            //TODO - Redirecionar para a tela de sucesso
            catch (Exception e)
            {
                await Shell.Current.GoToAsync($"ticket/payment/failed?number={Ticket.Number}&message={e.Message}");
            }
        }

        private string Valid(CreditCard creditCard)
        {
            StringBuilder messages = new StringBuilder();
            if (string.IsNullOrEmpty(CreditCard.Name))
            {
                messages.Append("Nome não preenchido!" + Environment.NewLine);
            }

            if (string.IsNullOrEmpty(CreditCard.Number))
            {
                messages.Append("Numero não preenchido!" + Environment.NewLine);
            }
            else if (CreditCard.Number.Length < 19)
            {
                messages.Append("O numero do cartão está incompleto!" + Environment.NewLine);
            }

            try
            {
                var expiredString = creditCard.DateExpired.Split('/');
                var month = int.Parse(expiredString[0]);
                var year = int.Parse(expiredString[1]);

               var expiredDate = new DateTime( year, month, 01);
               
            }
            catch (Exception e)
            {
                messages.Append("A validade do cartão não é válida!" + Environment.NewLine);
            }

            if (string.IsNullOrEmpty(CreditCard.SecurityCode))
            {
                messages.Append("O código de segurança não foi preenchido!" + Environment.NewLine);
            }
            else if (creditCard.SecurityCode.Length < 3)
            {
                messages.Append("O numero do cartão está incompleto!" + Environment.NewLine);
            }

            if (string.IsNullOrEmpty(CreditCard.Document))
            {
                messages.Append("O CPF não foi preenchdo!" + Environment.NewLine);
            }
            else if (creditCard.Document.Length < 14)
            {
                messages.Append("O CPF está incompleto!" + Environment.NewLine);
            }
            else if (!CPFValidator.IsCpf(CreditCard.Document))
            {
                messages.Append("CPF inválido!" + Environment.NewLine);
            }

            return messages.ToString();
        }
    }
}
