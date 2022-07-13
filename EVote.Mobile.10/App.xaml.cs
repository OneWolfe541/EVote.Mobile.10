using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using EVote.Settings;
using EVote.Utilities.Dialogs;
using EVote.Utilities.Views;

namespace EVote
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public SystemSettingsController GlobalSettings = new SystemSettingsController();

        public StatusBarViewModel StatusBar;

        public MainHeaderViewModel MainHeader;

        protected override void OnStartup(StartupEventArgs e)
        {
            Process proc = Process.GetCurrentProcess();
            int count = Process.GetProcesses().Where(p =>
                p.ProcessName == proc.ProcessName).Count();

            if (count > 1)
            {
                MessageBox.Show("An instance of EVote is already running");
                App.Current.Shutdown();
            }

            try
            {
                GlobalSettings.LoadJsonFile();

                // Check offline mode
                GlobalSettings.OfflineMode = GlobalSettings.Settings.OfflineMode;                
            }
            catch (Exception exception)
            {
                AlertDialog settingsFailed = new AlertDialog("SETTINGS FILE FAILED TO LOAD\r\n" + exception.Message);
                settingsFailed.ShowDialog();
                // Sample Shut down process
                // https://stackoverflow.com/questions/606043/shutting-down-a-wpf-application-from-app-xaml-cs
                Shutdown(1);
                return;
            }

            Window window = new MainWindow();
            window.DataContext = new MainWindowViewModel(window);
            window.Show();

            // Create Connection String from appsettings.json
            // https://stackoverflow.com/questions/59909207/cannot-add-appsettings-json-inside-wpf-project-net-core-3-0

            base.OnStartup(e);
        }
    }
}
