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
    /// Interaction logic for QuestionAdditionScreen.xaml
    /// </summary>
    public partial class QuestionAdditionScreen : Window
    {
        private Communicator _coms;
        public QuestionAdditionScreen(Communicator communicator)
        {
            InitializeComponent();
            _coms = communicator;
        }

        private void BSubmit_Click(object sender, RoutedEventArgs e)
        {
            /*messageInfo info = _coms.addNewQuestionRequest(IQuestion.Text, ICorrectAns.Text, ISecAns.Text, IThirdAns.Text, IFourthAns.Text);
            if (info.Code == 1)
            {
                MessageBox.Show(info.Json.ToString());
            }
            else
            {
                MessageBox.Show("Your question Has Been Submited!");
            }*/
        }
    }
}
