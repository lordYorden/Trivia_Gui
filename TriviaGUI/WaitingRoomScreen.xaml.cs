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
using System.ComponentModel;
using System.Threading;

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
        private bool _hasGameBegun;
        private Communicator _coms;
        private BackgroundWorker timer = new BackgroundWorker();
        public WaitingRoomScreen(RoomData metadata, bool isAdmin, Communicator communicator)
        {
            InitializeComponent();
            timer.DoWork += Timer_DoWork;
            timer.ProgressChanged += Timer_ProgressChanged;
            timer.WorkerSupportsCancellation = true;
            timer.WorkerReportsProgress = true;

            _currPlayers = 0;
            _coms = communicator;
            _isAdmin = isAdmin;
            _metadata = metadata;
            LRoomName.Text = "Room Name: " + _metadata.RoomName;
            timer.RunWorkerAsync();

            if (!_isAdmin)
                BStart.Visibility = Visibility.Hidden;

        }

        private void Timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            updateRoomState();
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

        public void updateRoomState() 
        {
            messageInfo info = _coms.getRoomStateRequest();
            /*MessageBox.Show(info.Json.ToString());*/

            try
            {
                JObject room = info.Json;
                _metadata.TimePerQuestion = Convert.ToInt32(room["answerTimeout"].ToString());
                _hasGameBegun = Convert.ToBoolean(room["hasGameBegun"].ToString());
                _metadata.QuestionCount = Convert.ToInt32(room["questionCount"].ToString());
                int status = Convert.ToInt32(room["status"].ToString());
                List<string> players = info.Json["players"].ToString().Split('-').ToList();
                if (status == 1)
                {
                    this.Close();
                }
                updatePlayers(players);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void WaitingRoomScreen_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            timer.CancelAsync();
            if (_isAdmin)
                _coms.closeRoomRequest();
            else
                _coms.leaveRoomRequest();
        }
    }
}
