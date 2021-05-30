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

namespace TriviaGUI
{
    /// <summary>
    /// Interaction logic for RoomSelectionScreen.xaml
    /// </summary>
    public partial class RoomSelectionScreen : Window
    {
        private String test;
        private Communicator _coms;
        public RoomSelectionScreen(Communicator communicator)
        {

            InitializeComponent();
            test = "Faild";
            _coms = communicator;
            messageInfo info= _coms.getRoomsRequest();
            MessageBox.Show(info.Json.ToString());
            JToken info1 = info.Json["Rooms"];
            
            foreach (var word in info1.ToString().Split('-'))
            {
                MessageBox.Show(word);
                DisplayRooms.Children.Add(CreateRoom(word));
            }
        }

        private void BTest_Click(object sender, RoutedEventArgs e)
        {
            string roomName = "This A realy Long Name"; //Max room name length 22 cherecters
            DisplayRooms.Children.Add(CreateRoom(roomName));
            
        }
        private StackPanel CreateRoom(String roomName)
        {
            //the object created
            //<StackPanel x:Name="Room" Orientation="Horizontal">
            //    <TextBlock Text="Name" Margin="10" Style="{DynamicResource TextStyle}"/>
            //    <Button Margin="10" Width="100" FontSize="24" Content="Join" Style="{DynamicResource BlueButtonStyle}"/>
            //</StackPanel>
            StackPanel room = new StackPanel();
            room.Orientation = Orientation.Horizontal;
            TextBlock lRoomName = new TextBlock();
            Button bJoinRoom = new Button();
            bJoinRoom.Margin = new Thickness(10);
            bJoinRoom.FontSize = 24;
            bJoinRoom.Content = "Join";
            bJoinRoom.Width = 100;
            Style blueStyle = this.FindResource("BlueButtonStyle") as Style;
            bJoinRoom.Style = blueStyle;
            bJoinRoom.Click += JoinRoom_Click;

            lRoomName.Text = roomName;
            lRoomName.Margin = new Thickness(10);
            Style textStyle = this.FindResource("TextStyle") as Style;
            lRoomName.Style = textStyle;
            room.Children.Add(lRoomName);
            room.Children.Add(bJoinRoom);
            return room;
        }
        private void JoinRoom_Click(object sender, RoutedEventArgs e)
        {
            test = "Working";
            MessageBox.Show(test);
            test = "Faild";
        }
    }
}
