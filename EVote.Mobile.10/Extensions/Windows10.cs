using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace EVote.Extensions
{
    public static class Windows10
    {
        public static bool IsWindows10()
        {
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            string productName = (string)reg.GetValue("ProductName");

            return productName.StartsWith("Windows 10");
        }
    }
}
