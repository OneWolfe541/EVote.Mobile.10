using EVote.Utilities.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EVote.Utilities.Views
{
    /// <summary>
    /// Interaction logic for VoterNameSearchView.xaml
    /// </summary>
    public partial class VoterNameSearchView : UserControl
    {
        private readonly ITouchKeyboardProvider _touchKeyboardProvider = new TouchKeyboardProvider();

        public VoterNameSearchView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LastNameText.IsEnabled = false;
            LastNameText.IsEnabled = true;
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            //var control = sender as Control;
            //var isTabStop = control.IsTabStop;
            //control.IsTabStop = false;
            //control.IsEnabled = false;
            //control.IsEnabled = true;
            //control.IsTabStop = isTabStop;

            this.Focus();
        }

        private void LastNameText_GotFocus(object sender, RoutedEventArgs e)
        {
            _touchKeyboardProvider.ShowTouchKeyboard();
        }

        private void GoButton_GotFocus(object sender, RoutedEventArgs e)
        {
            _touchKeyboardProvider.HideTouchKeyboard();
        }
    }
}
