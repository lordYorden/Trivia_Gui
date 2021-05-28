using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGUI
{
    public class Requests
    {

    }

    public class Login : Requests
    {
        private string name;
        private string password;

        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
    }
    public class signUp : Requests
    {
        private string name;
        private string password;
        private string email;

        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
    }
    public class getPlayers : Requests
    {
        private string roomId;

        public string RoomId { get => roomId; set => roomId = value; }
    }
    public class joinRoom : Requests
    {
        private string roomId;

        public string RoomId { get => roomId; set => roomId = value; }
    }
    public class createRoom : Requests
    {
        private string roomName;
        private int maxUsers;
        private int questionCount;
        private int answerTimeOut;

        public string RoomName { get => roomName; set => roomName = value; }
        public int MaxUsers { get => maxUsers; set => maxUsers = value; }
        public int QuestionCount { get => questionCount; set => questionCount = value; }
        public int AnswerTimeOut { get => answerTimeOut; set => answerTimeOut = value; }
    }
    
}
