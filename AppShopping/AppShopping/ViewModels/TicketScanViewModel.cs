using AppShopping.Libary.Helpers.MVVM;
using AppShopping.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace AppShopping.ViewModels
{
    public class TicketScanViewModel : BaseViewModel
    {
        private string _ticketNumber;

        public string TicketNumber { get
            {
                return _ticketNumber;
            } set
            {
                SetProperty(ref _ticketNumber, value);
            } }
        public ICommand TicketScanCommand { get; set; }
        public ICommand TicketTextChangedCommand { get; set; }
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
            TicketScanCommand = new MvvmHelpers.Commands.AsyncCommand(TicketScan);
            TicketTextChangedCommand = new Command(TicketTextChange);
            TicketPaidHistoryCommand = new Command(TicketPaidHistory);
        }

        private async void TicketTextChange()
        {
           if(TicketNumber.Length == 15)
            {
                var ticketNumber = TicketNumber.Replace(" ", string.Empty);
                await TicketProccess(ticketNumber);
            }
        }

        private void TicketPaidHistory()
        {
            Shell.Current.GoToAsync("ticket/paid/history");
           
        }

        private async Task TicketProccess(string ticketNumber)
        {
            try
            {
                new TicketService().GetTicketToPaid(ticketNumber);
                await Shell.Current.GoToAsync($"ticket/payment?number={ticketNumber}");
                TicketNumber = string.Empty;
            }
            catch(Exception e)
            {
                Message = e.Message;
            }
        }
        private async Task TicketScan()
        {
            var scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += async (result) =>
            {
                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () => {
                    await Shell.Current.Navigation.PopAsync();
                    Message = result.Text;
                    await TicketProccess(Message);
                });

            };

            await Shell.Current.Navigation.PushAsync(scanPage);
            
        }
    }
}
