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
        public GameScreen(RoomData metadata, Communicator communicator)
        {
            InitializeComponent();
            _coms = communicator;
            _currQuestionCount = 1;
            _metadata = metadata;

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
            LQuestionCount.Text = "Question " + _currQuestionCount;
            _currQuestionCount++;
            LQuestion.Text = _questionData.Question;
            LAnswer2.Content = _questionData.CorrectAnswer;
            LAnswer3.Content = _questionData.OtherAnswers[0];
            LAnswer4.Content = _questionData.OtherAnswers[1];
            LAnswer1.Content = _questionData.OtherAnswers[2];
            LTimer.Text = _metadata.TimePerQuestion.ToString();
        }
    }
}
