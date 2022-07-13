using EVote.Factories;
using EVote.Logging;
using EVote.Methods;
using EVote.Utilities.Dialogs;
using System;
using System.Collections.Generic;
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

namespace EVote.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();

            StatusBarMethods.ShowMenu(false);

            string systemName = System.Environment.MachineName;
            
        }

        private async void TestAPI()
        {
            ElectionFactory factory = new ElectionFactory();
            var test = await factory.BallotStyles();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            StatusBarMethods.Working = true;
            StatusBarMethods.Textleft = "Validating User";

            Login();
        }

        private async void Login()
        {
            await Task.Delay(1000);

            EVoteLogger _loginLogger = new EVoteLogger("EVoteLogs", true);
            _loginLogger.WriteLog("User Login Attempt");

            OfflineFactory factory = new OfflineFactory();
            var user = await factory.CompareLocations(UserNameText.Text, PasswordText.Password);
            if (user != null)
            {
                _loginLogger.WriteLog("User Found: " + user.LocationId.ToString());

                if (user.RollId == 0)
                {
                    StatusBarMethods.SetMenuAdmin(true);
                }

                AppSettings.SetUser(user);

                _loginLogger.WriteLog("Computer Name: " + System.Environment.MachineName);

                // Load election configs from API or DB
                StatusBarMethods.Textleft = "Loading Election Settings";
                _loginLogger.WriteLog("Loading Election Settings");
                AppSettings.Config = await ElectionConfigs.LoadAsync();

                // Load Location Jurisdictions
                if (AppSettings.Config != null && AppSettings.Config.DistrictOnlyVoting == true)
                {
                    AppSettings.Location = await ValidLocationMethods.LoadAsync();
                }

                // Load Printers
                StatusBarMethods.Textleft = "Loading Printers";
                _loginLogger.WriteLog("Loading Printers");
                await AppSettings.LoadPrinters();

                StatusBarMethods.ShowMenu(true);

                StatusBarMethods.Textleft = "";
                StatusBarMethods.Working = false;
                _loginLogger.WriteLog("Login Complete");

                Navigation.VoterSearchView();
            }
            else
            {
                StatusBarMethods.Working = false;

                StatusBarMethods.Textleft = "Invalid Login";
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            AlertDialog lostFocus = new AlertDialog("The focus was lost");
            lostFocus.ShowDialog();
        }

        private void Text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                OkButton_Click(sender, e);
            }
        }
    }
}
