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
using System.Linq;

namespace TriviaGUI
{
    /// <summary>
    /// Interaction logic for WaitingRoomScreen.xaml
    /// </summary>
    public partial class WaitingRoomScreen : Window
    {
        private int _currPlayers;
        private List<string> _players;
        private bool _isAdmin;
        private RoomData _metadata;
        private Communicator _coms;
        public WaitingRoomScreen(RoomData metadata, bool isAdmin, Communicator communicator)
        {
            InitializeComponent();
            _currPlayers = 0;
            _coms = communicator;
            _isAdmin = isAdmin;
            _metadata = metadata;

            LRoomName.Text = "Room Name: " + _metadata.RoomName;
            messageInfo info = _coms.getPlayersRequest(metadata.Id);
            MessageBox.Show(info.Json.ToString());

            List<string> players = info.Json["Players"].ToString().Split('-').ToList();
            updatePlayers(players);

            if (!_isAdmin)
                BStart.Visibility = Visibility.Hidden;

        }

        private void updatePlayersCount()
        {
            LplayerCount.Text = $"{_currPlayers}/{_metadata.MaxUsers}";
        }

        public void setMaxPlayerCount(int max)
        {
            _metadata.MaxUsers = max;
            updatePlayersCount();
        }
        public void setcurrPlayerCount(int curr)
        {
            _currPlayers = curr;
            updatePlayersCount();
        }

        public void updatePlayers(List<string> players)
        {
            _players = players;
            LAdminName.Text = "Admin: " + players.ElementAt(0);
            _currPlayers = players.Count();
            updatePlayersCount();
            LPlayers.Text = "Players: " + String.Join(", ", _players);
        }

        private void WaitingRoomScreen_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_isAdmin)
                _coms.closeRoomRequest();
            else
                _coms.leaveRoomRequest();
        }
    }
}
