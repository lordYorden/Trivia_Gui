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
using System.Windows.Shapes;

namespace TriviaGUI
{
    /// <summary>
    /// Interaction logic for SignupScreen.xaml
    /// </summary>
    public partial class SignupScreen : Window
    {
        private Communicator _coms;
        public SignupScreen(Communicator communicator)
        {
            _coms = communicator;
            InitializeComponent();
        }

        private void BCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BSignup_Click(object sender, RoutedEventArgs e)
        {
            string uname = IUsername.Text;
            string password = IPassword.Password;
            string mail = IEmail.Text;
            messageInfo info = _coms.signUpRequest(uname, password, mail);
            if (info.Code == 1)
            {
                MessageBox.Show(info.Json.ToString());
            }
            else
            {
                MainMenuScreen mainMenuScreen = new MainMenuScreen(uname, _coms);
                mainMenuScreen.Owner = this;
                Visibility = Visibility.Hidden;
                mainMenuScreen.ShowDialog();
                this.Close();
            }
            
        }
    }
}
