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
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void BLogin_Click(object sender, RoutedEventArgs e)
        {
            string uname = IUsername.Text;
            //login attempt here
            MainMenuScreen mainMenuScreen = new MainMenuScreen(uname);
            Visibility = Visibility.Hidden;
            mainMenuScreen.ShowDialog();
            this.Close();
        }

        private void BSignup_Click(object sender, RoutedEventArgs e)
        {
            SignupScreen signupScreen = new SignupScreen();
            Visibility = Visibility.Hidden;
            signupScreen.ShowDialog();
            this.Close();
        }
    }
}
