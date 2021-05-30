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
        private int maxPlayers;
        private int currPlayers;
        private List<string> players;
        private bool isAdmin;
        public WaitingRoomScreen(bool isAdmin)
        {
            InitializeComponent();
            currPlayers = 0;

            this.isAdmin = isAdmin;
            if (!isAdmin)
                BStart.Visibility = Visibility.Hidden;

        }

        private void updatePlayersCount()
        {
            LplayerCount.Text = $"{currPlayers}/{maxPlayers}";
        }

        public void setMaxPlayerCount(int max)
        {
            maxPlayers = max;
            updatePlayersCount();
        }
        public void setcurrPlayerCount(int curr)
        {
            currPlayers = curr;
            updatePlayersCount();
        }

        public void updatePlayers(List<string> players)
        {
            this.players = players;
            LAdminName.Text = "Admin: " + players.ElementAt(0);
            currPlayers = players.Count();
            updatePlayersCount();
            LPlayers.Text = "Players: " + String.Join(", ", this.players);
        }
    }
}
