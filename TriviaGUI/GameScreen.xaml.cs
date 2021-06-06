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
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : Window
    {
        private int _currQuestionCount;
        private RoomData _metadata;
        private QuestionData _questionData;
        private Communicator _coms;
        private List<Button> _bAnswers;
        private static Random rng = new Random();
        public GameScreen(RoomData metadata, Communicator communicator)
        {
            InitializeComponent();
            _coms = communicator;
            _currQuestionCount = 1;
            _metadata = metadata;
            _bAnswers = new List<Button>();
            _bAnswers.Add(LAnswerRed);
            _bAnswers.Add(LAnswerGreen);
            _bAnswers.Add(LAnswerYellow);
            _bAnswers.Add(LAnswerBlue);

            List<string> a = new List<string>();
            a.Add("Golda Meir");
            a.Add("Ariel Sharon");
            a.Add("Issac Shamir");
            string q = "Who was the first prime minister of israel?";
            string ca = "David Ben Gurion";

            _questionData = new QuestionData(q, ca, a);
            updateQuestion();

        }

        private void updateQuestion()
        {
            Random rnd = new Random();
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
        }

        private void displayCorrectAnswer() 
        {
            string bName = "";
            Style bStyle = null;
            for (int i = 0; i < _bAnswers.Count; i++)
            {
                if (_bAnswers[i].Content != _questionData.CorrectAnswer)
                {
                    bName = _bAnswers[i].Name;
                    if (bName == "LAnswerRed")
                        bStyle = this.FindResource("RedWorngButtonStyle") as Style;
                    else if (bName == "LAnswerGreen")
                        bStyle = this.FindResource("GreenWorngButtonStyle") as Style;
                    else if (bName == "LAnswerBlue")
                        bStyle = this.FindResource("BlueWorngButtonStyle") as Style;
                    else
                        bStyle = this.FindResource("YellowWorngButtonStyle") as Style;
                    _bAnswers[i].Style = bStyle;
                }
                _bAnswers[i].IsHitTestVisible = false;

            }
        }

        private void GameScreen_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _coms.leaveGameRequest();
        }

        private void LAnswerRed_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hI");
            displayCorrectAnswer();
        }
    }
}
