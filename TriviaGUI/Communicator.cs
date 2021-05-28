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
        public Communicator()
        {
           TcpClient client = new TcpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 200);
            client.Connect(serverEndPoint);
            clientStream = client.GetStream();
            
        }
        public string loginRequest(string name,string password)
        {
            JObject o = new JObject
            {
                {"name",name},
                {"password",password }
            };
            byte[] buffer = new ASCIIEncoding().GetBytes(Serializer.serializeResponse(1, o));
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
            buffer = new byte[4096];
            clientStream.Read(buffer, 0, 4096);
            Console.WriteLine(buffer);
            return "";
        }
    }
}
