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
    /// Interaction logic for PersonalStatisticsScreen.xaml
    /// </summary>
    public partial class PersonalStatisticsScreen : Window
    {
        private Communicator _coms;
        public PersonalStatisticsScreen(Communicator communicator)
        {
            InitializeComponent();
            _coms = communicator;
            messageInfo info = _coms.getStatisitcs();
            if (info.Code == 1)
            {
                MessageBox.Show(info.Json.ToString());
            }
            else
            {
                string[] stats = info.Json["Statistics"].ToString().Split('-');
                DAvgAswerTime.Text = stats[0];
                DTotalCorectAnswers.Text = stats[1];
                DTotalAnswers.Text = stats[2];
                DTimePlayed.Text = stats[3];
            }
        }
    }
}
