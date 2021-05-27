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
    /// Interaction logic for BestScoresScreen.xaml
    /// </summary>
    public partial class BestScoresScreen : Window
    {
        public BestScoresScreen()
        {
            InitializeComponent();
        }

        private void BTest_Click(object sender, RoutedEventArgs e)
        {
            string s = "1000";
            string n = "yarden";
            AddScore(n, s);
        }
        private void AddScore(String name, String score) 
        {
            Style textStyle = this.FindResource("TextStyle") as Style;
            TextBlock lscore = new TextBlock();
            lscore.Text = score;
            lscore.Margin = new Thickness(10);
            lscore.Style = textStyle;

            TextBlock lname = new TextBlock();
            lname.Text = name;
            lname.Margin = new Thickness(10);
            lname.Style = textStyle;
            DisplayTop5Names.Children.Add(lname);
            DisplayTop5Scores.Children.Add(lscore);
        }
    }
}
