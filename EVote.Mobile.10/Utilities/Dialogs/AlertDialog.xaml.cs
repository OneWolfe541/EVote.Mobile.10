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
using System.Windows.Shapes;

namespace EVote.Utilities.Dialogs
{
    /// <summary>
    /// Interaction logic for AlertDialog.xaml
    /// </summary>
    public partial class AlertDialog : Window
    {
        public AlertDialog(string message)
        {
            InitializeComponent();
            lblmessage.Content = message;

            Mouse.OverrideCursor = Cursors.Arrow;

            //this.Icon = FontAwesomeIcon.Exclamation;
            // Get icon images from https://paulferrett.com/fontawesome-favicon/
            Uri iconUri = new Uri("c://Program Files//EVote/Images/favicon-exclamation.ico");
            this.Icon = BitmapFrame.Create(iconUri);
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
