using AppShopping.Libary.Helpers.MVVM;
using AppShopping.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace AppShopping.ViewModels
{
    public class TicketScanViewModel : BaseViewModel
    {
        public string TicketNumber { get; set; }
        public ICommand TicketScanCommand { get; set; }
        public ICommand TicketPaidHistoryCommand { get; set; }
        private string _message;
        public string Message {
            get{
                return _message;
            }
            set {
                SetProperty(ref _message, value);
            }
        }

        public TicketScanViewModel()
        {
            TicketScanCommand = new Command(TicketScan);
            TicketPaidHistoryCommand = new Command(TicketPaidHistory);
        }

        private void TicketPaidHistory()
        {
            TicketProccess("");
        }

        private void TicketProccess(string ticketNumber)
        {
            try
            {
                new TicketService().GetTicketInfo(ticketNumber);

                //TODO - Navegar para a pagina de pagamento do ticket
            }
            catch(Exception e)
            {
                Message = e.Message;
            }
        }
        private void TicketScan()
        {
            var scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += async (result) =>
            {
                scanPage.IsScanning = false;

                 Shell.Current.Navigation.PopAsync();
                Message = result.Text;
            };

             Shell.Current.Navigation.PushAsync(scanPage);
            TicketProccess("");
        }
    }
}
