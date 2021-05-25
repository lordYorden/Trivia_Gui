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
        public MainMenuScreen(string uname)
        {
            InitializeComponent();
            _uname = uname;
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
            RoomSelectionScreen roomSelectionScreen = new RoomSelectionScreen();
            Visibility = Visibility.Hidden;
            roomSelectionScreen.ShowDialog();
            Visibility = Visibility.Visible;
        }
    }
}
