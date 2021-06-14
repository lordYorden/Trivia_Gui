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
            try
            {
                messageInfo info = _coms.createRoomRequest(IRoomName.Text, Int32.Parse(IMaxUsers.Text), Int32.Parse(IQuestionCount.Text), Int32.Parse(IQuestionTime.Text));
                if (info.Code == 1)
                {
                    MessageBox.Show(info.Json.ToString());
                }
                else
                {
                    int id = Int32.Parse(info.Json["status"].ToString());
                    RoomData roomInfo = new RoomData(IRoomName.Text, id, Int32.Parse(IMaxUsers.Text), Int32.Parse(IQuestionTime.Text), Int32.Parse(IQuestionCount.Text));
                    WaitingRoomScreen waitRoom = new WaitingRoomScreen(roomInfo, true, _coms);
                    Visibility = Visibility.Hidden;
                    waitRoom.ShowDialog();
                }
            }
            catch
            {
                MessageBox.Show("Invalid Credentials");
            }
            
            
            
        }
        
    }
}
