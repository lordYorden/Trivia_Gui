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
    /// Interaction logic for GameResultScreen.xaml
    /// </summary>
    public partial class GameResultScreen : Window
    {
        private Communicator _coms;
        private List<PlayerResults> _results;
        public GameResultScreen(Communicator communicator)
        {
            InitializeComponent();
            _coms = communicator;
            _results = Deserializer.desirializeGameResult(_coms.getGameResultsRequest().Json);
            updateResults();
        }

        public void updateResults()
        {
            for (int i = 0; i < _results.Count; i ++)
            {
                addResult(i+1, _results[i].Uname, _results[i].Score);
            }
        }

        public void addResult(int place, string uname, string score)
        {
            /*<TextBlock Margin="10" Text="yarden" HorizontalAlignment="Left" FontSize="30" Style="{DynamicResource TextStyle}"/>*/
            TextBlock lplace = new TextBlock();
            lplace.Margin = new Thickness(10);
            lplace.HorizontalAlignment = HorizontalAlignment.Left;
            lplace.FontSize = 30;
            Style textStyle = this.FindResource("TextStyle") as Style;
            lplace.Style = textStyle;
            lplace.Text = "#" + place.ToString();
            places.Children.Add(lplace);

            TextBlock luname = new TextBlock();
            luname.Margin = new Thickness(10);
            luname.HorizontalAlignment = HorizontalAlignment.Left;
            luname.FontSize = 30;
            luname.Style = textStyle;
            luname.Text = uname;
            unames.Children.Add(luname);

            TextBlock lscore = new TextBlock();
            lscore.Margin = new Thickness(10);
            lscore.HorizontalAlignment = HorizontalAlignment.Left;
            lscore.FontSize = 30;
            lscore.Style = textStyle;
            lscore.Text = score;
            scores.Children.Add(lscore);
        }
    }
}
