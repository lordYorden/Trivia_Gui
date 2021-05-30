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
    /// Interaction logic for RoomCreationScreen.xaml
    /// </summary>
    public partial class RoomCreationScreen : Window
    {
        private Communicator _coms;
        public RoomCreationScreen(Communicator communicator)
        {
            
            InitializeComponent();
            _coms = communicator;
           
        }

        private void BCreate_Click(object sender, RoutedEventArgs e)
        {
            messageInfo info = _coms.createRoomRequest(IRoomName.Text, Int32.Parse(IMaxUsers.Text), Int32.Parse(IQuestionCount.Text), Int32.Parse(IQuestionTime.Text));
            MessageBox.Show(info.Json.ToString());
        }
        
    }
}
