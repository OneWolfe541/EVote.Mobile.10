using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using EVote.Utilities.Views;
using EVote.Views.Main;

namespace EVote
{
    public class MainWindowViewModel : ApplicationViewModelBase
    {
        public MainWindowViewModel(Window window)
        {
            ApplicationView = new MainView();
            ApplicationView.DataContext = new MainViewModel(window);
        }
    }
}
