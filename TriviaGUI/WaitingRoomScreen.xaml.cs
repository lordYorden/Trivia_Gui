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
    /// Interaction logic for WaitingRoomScreen.xaml
    /// </summary>
    public partial class WaitingRoomScreen : Window
    {
        private int _maxPlayers;
        private int _currPlayers;
        private List<string> _players;
        private bool _isAdmin;
        private int _roomID;
        private string _roomName;
        private Communicator _coms;
        public WaitingRoomScreen(string roomName, int roomID, bool isAdmin, Communicator communicator)
        {
            InitializeComponent();
            _currPlayers = 0;
            _coms = communicator;
            _isAdmin = isAdmin;
            _roomName = roomName;
            _roomID = roomID;
            if (!_isAdmin)
                BStart.Visibility = Visibility.Hidden;

        }

        private void updatePlayersCount()
        {
            LplayerCount.Text = $"{_currPlayers}/{_maxPlayers}";
        }

        public void setMaxPlayerCount(int max)
        {
            _maxPlayers = max;
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
    }
}
