using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGUI
{
    public class RoomData
    {
        private string roomName;
        private int id;
        private int maxUsers;
        private int timePerQuestion;
        private int questionCount;

        public RoomData(string roomName, int id, int maxUsers, int timePerQuestion, int questionCount)
        {
            this.roomName = roomName;
            this.id = id;
            this.maxUsers = maxUsers;
            this.timePerQuestion = timePerQuestion;
            this.questionCount = questionCount;
        }

        public string RoomName { get => roomName; set => roomName = value; }
        public int Id { get => id; set => id = value; }
        public int MaxUsers { get => maxUsers; set => maxUsers = value; }
        public int TimePerQuestion { get => timePerQuestion; set => timePerQuestion = value; }
        public int QuestionCount { get => questionCount; set => questionCount = value; }
    }

    public class QuestionData 
    {
        private string question;
        private string correctAnswer;
        List<string> otherAnswers;

        public QuestionData(string question, string correctAnswer, List<string> otherAnswers)
        {
            this.question = question;
            this.correctAnswer = correctAnswer;
            this.otherAnswers = otherAnswers;
        }

        public string Question { get => question; set => question = value; }
        public string CorrectAnswer { get => correctAnswer; set => correctAnswer = value; }
        public List<string> OtherAnswers { get => otherAnswers; set => otherAnswers = value; }
    }
}
