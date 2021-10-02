using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ChatClient
{
    public class Client
    {
        private string Name { get; set; }
        private IPAddress ServerIP { get; set; }
        private IPEndPoint EndPoint { get; set; }
        private Socket socket;
        public Client(string serverIP, string port, string name = "Klient1")
        {   
            //Resolve the connection information
            IPHostEntry ipHost = Dns.GetHostEntry(serverIP);
            this.ServerIP = ipHost.AddressList[0];
            this.EndPoint = new IPEndPoint(this.ServerIP, 12345);
            this.Name = name;

            //Connect to server and start communication
            createNewSocket();
            connectToServer();
        }

        private void createNewSocket()
        {
            try
            {
                this.socket = new Socket(this.ServerIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void connectToServer()
        {
            try
            {
                //Establish connection
                socket.Connect(this.EndPoint);
                Console.WriteLine($"Connection with {this.EndPoint} established");

                //Send a message
                string msg = "This is a test";
                sendMsg(msg);

                //Wait for response
                handleIncomingMessage();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void handleIncomingMessage()
        {
            try
            {
                //Receive encoded data from server;
                byte[] encodedMsg = new byte[1024];
                this.socket.Receive(encodedMsg);

                //Decode data
                string msg = Encoding.ASCII.GetString(encodedMsg);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void sendMsg(string msg)
        {
            try
            {
                //Encode data
                byte[] encodedMsg = Encoding.ASCII.GetBytes(msg);
                this.socket.Send(encodedMsg);
                Console.WriteLine($"{this.Name}: {msg}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
