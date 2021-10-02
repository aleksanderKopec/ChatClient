using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ChatServer
{
    class Client
    {
        private string Name { get; set; }
        private IPAddress ServerIP { get; set; }
        private IPEndPoint EndPoint { get; set; }
        private Socket socket;
        Client(string serverIP, string port, string name = "Klient1")
        {
            IPHostEntry ipHost = Dns.GetHostEntry(serverIP);
            this.ServerIP = ipHost.AddressList[0];
            this.EndPoint = new IPEndPoint(this.ServerIP, 12345);
            this.Name = name;
        }

        private void createNewSocket()
        {
            try
            {
                this.socket.Close();
                this.socket = new Socket(this.ServerIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void connectToServer()
        {
            byte[] receivedMessage = new byte[1024];
            try
            {
                //Establish connection
                socket.Connect(this.EndPoint);
                Console.WriteLine($"Connection with {this.EndPoint} established");

                //Send a message
                string msg = "This is a test";
                sendMsg(msg);
            }
        }

        private void handleIncomingMessage()
        {
            //Receive encoded data from server;

        }

        private void sendMsg(string msg)
        {
            //Encode data
            byte[] encodedMsg = Encoding.ASCII.GetBytes(msg);
            this.socket.Send(encodedMsg);
            Console.WriteLine
        }
    }
}
