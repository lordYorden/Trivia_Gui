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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TriviaGUI
{
    /// <summary>
    /// Interaction logic for BestScoresScreen.xaml
    /// </summary>
    public partial class BestScoresScreen : Window
    {
        private Communicator _coms;
        public BestScoresScreen(Communicator communicator)
        {
            InitializeComponent();
            _coms = communicator;
        }

        private void BTest_Click(object sender, RoutedEventArgs e)
        {

            messageInfo info = _coms.highScoreRequest();
            JToken info1 = info.Json["Highscores"];
            foreach(var word in info1.ToString().Split('-'))
            {
                if(word != "")
                {
                    string[] words = word.Split('=');
                    AddScore(words[0], words[1]);
                }
                
            }
            
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
