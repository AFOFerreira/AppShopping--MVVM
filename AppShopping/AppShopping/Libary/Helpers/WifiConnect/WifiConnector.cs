using System;
using System.Collections.Generic;
using System.Text;

namespace AppShopping.Libary.Helpers.WifiConnect
{
    public interface WifiConnector
    {
        void ConnectToWifi(string ssid, string password);
    }
}
