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
using System.Threading;
using System.ComponentModel;

namespace TriviaGUI
{
    /// <summary>
    /// Interaction logic for RoomSelectionScreen.xaml
    /// </summary>
    public partial class RoomSelectionScreen : Window
    {
        private Dictionary<string, RoomData> _roomNameToID;
        private Communicator _coms;
        private BackgroundWorker timer = new BackgroundWorker();
        public RoomSelectionScreen(Communicator communicator)
        {
            InitializeComponent();
            timer.DoWork += Timer_DoWork;
            timer.ProgressChanged += Timer_ProgressChanged;
            timer.WorkerSupportsCancellation = true;
            timer.WorkerReportsProgress = true;

            _coms = communicator;
            _roomNameToID = new Dictionary<string, RoomData>();
            timer.RunWorkerAsync();
        }

        private void Timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            updateRooms();
        }

        private void Timer_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                timer.ReportProgress(0);
                System.Threading.Thread.Sleep(3000);

                if (timer.CancellationPending)
                {
                    Console.WriteLine("room is closeing....");
                    e.Cancel = true;
                    break;
                }
            }
        }

        private void updateRooms()
        {
            messageInfo info = _coms.getRoomsRequest();
            /* MessageBox.Show(info.Json.ToString());*/
            JToken rooms = info.Json["Rooms"];
            string roomsData = rooms.ToString();
            roomsData = roomsData.Substring(1, roomsData.Length - 2);
            /*MessageBox.Show(roomsData);*/

            foreach (String roomData in roomsData.Split('-'))
            {
                if (roomData != "")
                {
                    try
                    {
                        JObject room = JObject.Parse(roomData);
                        int roomID = Convert.ToInt32(room["id"].ToString());
                        string roomName = room["name"].ToString();
                        int timePerQuestion = Convert.ToInt32(room["timePerQuestion"].ToString());
                        int maxPlayers = Convert.ToInt32(room["maxPlayers"].ToString());
                        int questionCount = Convert.ToInt32(room["questionCount"].ToString());

                        RoomData metadata = new RoomData(roomName, roomID, maxPlayers, timePerQuestion, questionCount);

                        /*MessageBox.Show($"RoomId: {roomID} RoomName: {roomName}");*/
                        DisplayRooms.Children.Add(CreateRoom(roomName, metadata));
                    }
                    catch (Exception e)
                    {
                        /*MessageBox.Show(e.Message);*/
                    }
                }

            }
        }

        private void BTest_Click(object sender, RoutedEventArgs e)
        {
            /*string roomName = "This A realy Long Name"; //Max room name length 22 cherecters
            DisplayRooms.Children.Add(CreateRoom(roomName, ));*/
        }
        private StackPanel CreateRoom(string roomName , RoomData metadata)
        {
            _roomNameToID.Add(roomName, metadata);

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
            Button bsender = sender as Button;
            StackPanel room = bsender.Parent as StackPanel;
            TextBlock lroomName = room.Children[0] as TextBlock;
            RoomData metadata = _roomNameToID[lroomName.Text];
            int roomID = metadata.Id;
            string roomName = lroomName.Text;
            /*MessageBox.Show($"Room Name: {roomName}, RoomID: {roomID}");*/
            
            WaitingRoomScreen waitRoom = new WaitingRoomScreen(metadata, false,_coms);
            Visibility = Visibility.Hidden;
            waitRoom.ShowDialog();
            Visibility = Visibility.Visible;
        }

        private void RoomSelectionScreen_Closing(object sender, CancelEventArgs e)
        {
            timer.CancelAsync();
        }
    }
}
