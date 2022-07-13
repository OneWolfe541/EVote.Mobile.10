using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for DateSearchYearView.xaml
    /// </summary>
    public partial class DateSearchYearView : UserControl
    {
        public DateSearchYearView()
        {
            InitializeComponent();
        }

        // https://stackoverflow.com/questions/2292360/how-to-control-the-scroll-position-of-a-listbox-in-a-mvvm-wpf-app
        private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Selector selector = sender as Selector;
            if (selector is ListBox)
            {
                (selector as ListBox).ScrollIntoView(selector.SelectedItem);
            }
        }
    }
}
