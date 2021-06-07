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
using System.ComponentModel;

namespace TriviaGUI
{
    /// <summary>
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : Window
    {
        private int _currQuestionCount;
        private RoomData _metadata;
        private QuestionData _questionData;
        private Communicator _coms;
        private List<Button> _bAnswers;
        private BackgroundWorker _timer = new BackgroundWorker();
        private BackgroundWorker _cooldown = new BackgroundWorker();
        private static Random rng = new Random();
        private bool _hasGameStoped;
        public GameScreen(RoomData metadata, Communicator communicator)
        {
            InitializeComponent();
            _timer.DoWork += Timer_DoWork;
            _timer.ProgressChanged += Timer_ProgressChanged;
            _timer.RunWorkerCompleted += Timer_RunWorkerCompleted;
            _timer.WorkerSupportsCancellation = true;
            _timer.WorkerReportsProgress = true;

            _cooldown.DoWork += _cooldown_DoWork; ;
            _cooldown.RunWorkerCompleted += _cooldown_RunWorkerCompleted; ;

            _coms = communicator;
            _currQuestionCount = 1;
            _metadata = metadata;
            _hasGameStoped = false;
            _bAnswers = new List<Button>();
            _bAnswers.Add(LAnswerRed);
            _bAnswers.Add(LAnswerGreen);
            _bAnswers.Add(LAnswerYellow);
            _bAnswers.Add(LAnswerBlue);

            /*List<string> a = new List<string>();
            a.Add("Golda Meir");
            a.Add("Ariel Sharon");
            a.Add("Issac Shamir");
            string q = "Who was the first prime minister of israel?";
            string ca = "David Ben Gurion";

            _questionData = new QuestionData(q, ca, a);*/
            updateQuestion();

        }

        private void _cooldown_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            resetAnswers();
            updateQuestion();
        }

        private void _cooldown_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(Convert.ToInt32(e.Argument));
        }

        private void Timer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!_hasGameStoped)
            {
                if (!e.Cancelled)
                {
                    submitAndGetNextQuestion("", _metadata.TimePerQuestion);
                }
                displayCorrectAnswer();
                _cooldown.RunWorkerAsync(2000);
            }
        }

        private void Timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            LTimer.Text = e.ProgressPercentage.ToString();
        }

        private void Timer_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = Convert.ToInt32(e.Argument); i >= 0; i--)
            {
                _timer.ReportProgress(i);
                System.Threading.Thread.Sleep(1000);
            }

            if (_timer.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void updateQuestion()
        {
            if(_currQuestionCount > _metadata.QuestionCount)
            {
                _timer.CancelAsync();
                _hasGameStoped = true;
                //to game ending screen
                MessageBox.Show("Game has ended");
            }

            _questionData = Deserializer.desirializeQuestion(_coms.getQuestionRequest().Json);
            LQuestionCount.Text = "Question " + _currQuestionCount;
            _currQuestionCount++;
            LQuestion.Text = _questionData.Question;
            List<string> questions = new List<string>(_questionData.OtherAnswers);
            questions.Add(_questionData.CorrectAnswer);
            questions = questions.OrderBy(a => rng.Next()).ToList();
            for (int i = 0; i < _bAnswers.Count; i++)
            {
                _bAnswers[i].Content = questions[i];
            }
            LTimer.Text = _metadata.TimePerQuestion.ToString();
            _timer.RunWorkerAsync(_metadata.TimePerQuestion);
        }

        private void submitAndGetNextQuestion(string ans, int ansTime)
        {
            _coms.submitAnswerRequest(ans, ansTime);
            lockAnswer(ans);
            if (_timer.IsBusy)
                _timer.CancelAsync();
        }

        private void displayCorrectAnswer()
        {
            for (int i = 0; i < _bAnswers.Count; i++)
            {
                if (_bAnswers[i].Content != _questionData.CorrectAnswer)
                {
                    _bAnswers[i].Opacity = 0.5;
                }
                else
                {
                    _bAnswers[i].Opacity = 1;
                }
                _bAnswers[i].IsHitTestVisible = false;

            }
        }

        private void lockAnswer(string ans)
        {
            for (int i = 0; i < _bAnswers.Count; i++)
            {
                if (_bAnswers[i].Content != ans)
                {
                    _bAnswers[i].Opacity = 0.5;
                }
                _bAnswers[i].IsHitTestVisible = false;

            }
        }

        private void resetAnswers()
        {
            for (int i = 0; i < _bAnswers.Count; i++)
            {
                _bAnswers[i].Opacity = 1;
                _bAnswers[i].IsHitTestVisible = true;
            }
        }


        private void LAnswerRed_Click(object sender, RoutedEventArgs e)
        {
            submitAndGetNextQuestion(LAnswerRed.Content.ToString(), Convert.ToInt32(LTimer.Text));
        }

        private void LAnswerGreen_Click(object sender, RoutedEventArgs e)
        {
            submitAndGetNextQuestion(LAnswerGreen.Content.ToString(), Convert.ToInt32(LTimer.Text));
        }

        private void LAnswerYellow_Click(object sender, RoutedEventArgs e)
        {
            submitAndGetNextQuestion(LAnswerYellow.Content.ToString(), Convert.ToInt32(LTimer.Text));
        }

        private void LAnswerBlue_Click(object sender, RoutedEventArgs e)
        {
            submitAndGetNextQuestion(LAnswerBlue.Content.ToString(), Convert.ToInt32(LTimer.Text));
        }
        private void GameScreen_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _timer.CancelAsync();
            _hasGameStoped = true;
            _coms.leaveGameRequest();
        }

    }
}
