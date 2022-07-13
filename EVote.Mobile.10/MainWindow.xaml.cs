using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EVote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Maximize the window to fit with Task Bar
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;

            string server = System.Environment.MachineName;
            if (server != "GARYC-WIN7" && server != "GARYC-DT10")
            {
                MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight - 15;
                MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;

                Height = 1280; 
                Width = 1920;
                ResizeMode =  ResizeMode.NoResize;
                WindowState = WindowState.Maximized;
            }
        }

        private void MainWindowGrid_Loaded(object sender, RoutedEventArgs e)
        {
            this.MouseDown += delegate { DragMove(); };
        }

        // Maximize the window when mouse double clicks in the title bar area
        private void MaximizeMyWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && e.GetPosition(this).Y < 75)
            {
                WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;

                if (this.WindowState == WindowState.Maximized)
                {
                    this.BorderThickness = new Thickness(8);
                }
                else
                {
                    this.BorderThickness = new Thickness(0);
                }
            }
        }

        private void Maximize()
        {
            WindowState = WindowState.Maximized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Drag the main window when left mouse button is pressed in the title bar area
            if (e.ChangedButton == MouseButton.Left && e.GetPosition(this).Y < 75)
            {
                DragMove();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.F5)
            {
                e.Handled = true;
            }
        }
    }
}
