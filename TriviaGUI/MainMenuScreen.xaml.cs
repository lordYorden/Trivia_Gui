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
    /// Interaction logic for MainMenuScreen.xaml
    /// </summary>
    public partial class MainMenuScreen : Window
    {
        private string _uname;
        private  Communicator _coms;
        public MainMenuScreen(string uname,  Communicator communicator)
        {
            InitializeComponent();
            _uname = uname;
            _coms = communicator;
            Debug.Text = uname;
        }

        private void BMyStats_Click(object sender, RoutedEventArgs e)
        {
            PersonalStatisticsScreen personalStatisticsScreen = new PersonalStatisticsScreen();
            Visibility = Visibility.Hidden;
            personalStatisticsScreen.ShowDialog();
            Visibility = Visibility.Visible;
        }

        private void BJoinRoom_Click(object sender, RoutedEventArgs e)
        {
            RoomSelectionScreen roomSelectionScreen = new RoomSelectionScreen(_coms);
            Visibility = Visibility.Hidden;
            roomSelectionScreen.ShowDialog();
            Visibility = Visibility.Visible;
        }

        private void BCreateRoom_Click(object sender, RoutedEventArgs e)
        {
            RoomCreationScreen roomCreationScreen = new RoomCreationScreen(_coms);
            Visibility = Visibility.Hidden;
            roomCreationScreen.ShowDialog();
            Visibility = Visibility.Visible;
        }

        private void BBestScore_Click(object sender, RoutedEventArgs e)
        {
            BestScoresScreen bestScoresScreen = new BestScoresScreen(_coms);
            Visibility = Visibility.Hidden;
            bestScoresScreen.ShowDialog();
            Visibility = Visibility.Visible;
        }
    }
}
