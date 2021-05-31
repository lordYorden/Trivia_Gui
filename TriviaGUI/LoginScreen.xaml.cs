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

namespace TriviaGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        private Communicator _coms;
        public LoginScreen()
        {
            InitializeComponent();
            try
            {
                this._coms = new Communicator();
            }
            catch
            {
                MessageBox.Show("Unable To Connect To the server...Try Again Later!");
                this.Close();
            }
        }

        private void BLogin_Click(object sender, RoutedEventArgs e)
        {
            string uname = IUsername.Text;
            string password = IPassword.Password;
            messageInfo info =_coms.loginRequest(uname, password);
           
            if(info.Code == 1 )
            {
                MessageBox.Show(info.Json.ToString());
            }
            else
            {
                MainMenuScreen mainMenuScreen = new MainMenuScreen(uname, _coms);
                mainMenuScreen.Owner = this;
                Visibility = Visibility.Hidden;
                mainMenuScreen.ShowDialog();
            }
            
        }

        private void BSignup_Click(object sender, RoutedEventArgs e)
        {
            SignupScreen signupScreen = new SignupScreen(_coms);
            Visibility = Visibility.Hidden;
            signupScreen.ShowDialog();
            Visibility = Visibility.Visible;
        }
    }
}
