﻿using System;
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
        private Dictionary<string, int> _roomNameToID;
        private Communicator _coms;
        public RoomSelectionScreen(Communicator communicator)
        {
            InitializeComponent();
            _coms = communicator;
            _roomNameToID = new Dictionary<string, int>();

            messageInfo info = _coms.getRoomsRequest();
/*            MessageBox.Show(info.Json.ToString());*/
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
                        MessageBox.Show($"RoomId: {roomID} RoomName: {roomName}");
                        DisplayRooms.Children.Add(CreateRoom(roomName, roomID));
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
                
            }
        }

        private void BTest_Click(object sender, RoutedEventArgs e)
        {
            string roomName = "This A realy Long Name"; //Max room name length 22 cherecters
            DisplayRooms.Children.Add(CreateRoom(roomName, 15));
        }
        private StackPanel CreateRoom(string roomName ,int roomId)
        {
            _roomNameToID.Add(roomName, roomId);

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
            int roomID = _roomNameToID[lroomName.Text];
            string roomName = lroomName.Text;
            MessageBox.Show($"Room Name: {roomName}, RoomID: {roomID}");
            Visibility = Visibility.Hidden;
            WaitingRoomScreen waitRoom = new WaitingRoomScreen();
        }
    }
}
