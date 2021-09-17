using AppShopping.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping.Services
{
    public class PaymentService
    {
        public string SendPayment(CreditCard creditCard, Ticket ticket)
        {
            if(creditCard.SecurityCode == "111")
            {
                throw new Exception("Código do cartão inválido");
                
            }
            
            return "1";
        }
    }
}
