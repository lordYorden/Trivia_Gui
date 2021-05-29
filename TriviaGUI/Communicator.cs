using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json.Linq;

namespace TriviaGUI
{
    enum RequestId
    {
        MT_RESPONSE_OK = 0,
        MT_LOGIN_REQUEST = 1,
        MT_SIGNUP_REQUEST = 2,
        MT_SIGNOUT_REQUEST = 3,
        MT_GET_ROOMS_REQUEST = 4,
        MT_GET_PLAYERS_IN_ROOM_REQUEST = 5,
        MT_GET_STATISTICS = 6,
        MT_GET_HIGHSCORES = 7,
        MT_JOIN_ROOM = 8,
        MT_CREATE_ROOM = 9,
        MT_CLOSE_ROOM = 10,
        MT_START_GAME = 11,
        MT_LEAVE_ROOM = 12,
        MT_GET_ROOM_STATE = 13,
        MT_EXIT = 99,
        MT_ERROR = 99
    };
    class Communicator
    {
        private NetworkStream clientStream;
        private TcpClient client;
        private IPEndPoint ipend;
        public Communicator()
        {
            this.client = new TcpClient();
            this.ipend = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 200);
            client.Connect(this.ipend);
            this.clientStream = this.client.GetStream();


        }
        public string loginRequest(string name, string password)
        {
            JObject o = new JObject
            {
                {"username",name},
                {"password",password }
            };

            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse((int)RequestId.MT_LOGIN_REQUEST, o));
            return sendToServer(buffer);


        }
        public string signUpRequest(string name, string password,string email)
        {
            JObject o = new JObject
            {
                {"username",name},
                {"password",password },
                {"mail",email }
            };
            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse((int)RequestId.MT_SIGNUP_REQUEST, o));
            return sendToServer(buffer);
        }
        public string getRoomsRequest()
        {
            JObject o = new JObject { };
            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse((int)RequestId.MT_GET_ROOM_STATE, o)); //sending code only
            return sendToServer(buffer);
        }
        public string joinRoomRequest(int roomId)
        {
            JObject o = new JObject
            {
                {"roomID",roomId }
            };
            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse((int)RequestId.MT_JOIN_ROOM, o));
            return sendToServer(buffer);
        }
        public string getPlayersRequest(int roomId)
        {
            JObject o = new JObject
            {
                {"roomID",roomId }
            };
            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse((int)RequestId.MT_GET_PLAYERS_IN_ROOM_REQUEST, o));
            return sendToServer(buffer);
        }
        public string signOutRequest()
        {
            JObject o = new JObject { };
            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse((int)RequestId.MT_SIGNOUT_REQUEST, o)); //sending code only
            return sendToServer(buffer);
        }
        public string createRoomRequest(string roomName, int maxUsers, int qCount, int answerTime)
        {
            JObject o = new JObject
            {
                {"roomName",roomName },
                { "maxUsers",maxUsers},
                {"questionCount",qCount },
                {"answerTimeout",answerTime }


            };
            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse((int)RequestId.MT_CREATE_ROOM, o));
            return sendToServer(buffer);
        }
        public string highScoreRequest()
        {
            JObject o = new JObject { };
            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse((int)RequestId.MT_GET_HIGHSCORES, o)); //sending code only
            return sendToServer(buffer);

        }
        public string getStatisitcs()
        {
            JObject o = new JObject { };
            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse((int)RequestId.MT_GET_STATISTICS, o)); //sending code only
            return sendToServer(buffer);
        }
        public string closeRoomRequest()
        {
            JObject o = new JObject { };
            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse((int)RequestId.MT_CLOSE_ROOM, o)); //sending code only
            return sendToServer(buffer);
        }
        public string startGameRequest()
        {
            JObject o = new JObject { };
            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse((int)RequestId.MT_START_GAME, o)); //sending code only
            return sendToServer(buffer);
        }
        public string leaveRoomRequest()
        {
            JObject o = new JObject { };
            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse((int)RequestId.MT_LEAVE_ROOM, o)); //sending code only
            return sendToServer(buffer);
        }
        public string getRoomStateRequest()
        {
            JObject o = new JObject { };
            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse((int)RequestId.MT_GET_ROOM_STATE, o)); //sending code only
            return sendToServer(buffer);
        }
        private string sendToServer(byte[] buffer)
        {
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
            buffer = new byte[4096];
            int bytesRead = clientStream.Read(buffer, 0, 4096);
            return System.Text.Encoding.UTF8.GetString(buffer);
        }
    }
    
}
