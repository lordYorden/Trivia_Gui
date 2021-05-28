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

            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse(1, o));
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
            buffer = new byte[4096];
            int bytesRead = clientStream.Read(buffer, 0, 4096);
            return System.Text.Encoding.UTF8.GetString(buffer);


        }
        public string signUpRequest(string name, string password,string email)
        {
            JObject o = new JObject
            {
                {"username",name},
                {"password",password },
                {"mail",email }
            };
            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse(2, o));
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
            buffer = new byte[4096];
            int bytesRead = clientStream.Read(buffer, 0, 4096);
            return System.Text.Encoding.UTF8.GetString(buffer);
        }
        public string getRooms()
        {
            byte[] buffer = new ASCIIEncoding().GetBytes("4"); //sending code only
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
            buffer = new byte[4096];
            int bytesRead = clientStream.Read(buffer, 0, 4096);
            return System.Text.Encoding.UTF8.GetString(buffer);
        }
        public string joinRoom(int roomId)
        {
            JObject o = new JObject
            {
                {"roomID",roomId }
            };
            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse(7,o)); 
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
            buffer = new byte[4096];
            int bytesRead = clientStream.Read(buffer, 0, 4096);
            return System.Text.Encoding.UTF8.GetString(buffer);
        }
        public string getPlayers(int roomId)
        {
            JObject o = new JObject
            {
                {"roomID",roomId }
            };
            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse(5, o));
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
            buffer = new byte[4096];
            int bytesRead = clientStream.Read(buffer, 0, 4096);
            return System.Text.Encoding.UTF8.GetString(buffer);
        }
        public string signOut()
        {
            byte[] buffer = new ASCIIEncoding().GetBytes("3"); //sending code only
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
            buffer = new byte[4096];
            int bytesRead = clientStream.Read(buffer, 0, 4096);
            return System.Text.Encoding.UTF8.GetString(buffer);
        }
        public string createRoom(string roomName, int maxUsers, int qCount, int answerTime)
        {
            JObject o = new JObject
            {
                {"roomName",roomName },
                { "maxUsers",maxUsers},
                {"questionCount",qCount },
                {"answerTimeout",answerTime }


            };
            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse(8, o));
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
            buffer = new byte[4096];
            int bytesRead = clientStream.Read(buffer, 0, 4096);
            return System.Text.Encoding.UTF8.GetString(buffer);
        }
    }
}
