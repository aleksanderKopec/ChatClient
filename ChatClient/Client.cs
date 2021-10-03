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

        private Socket connection { get; set; }
        public Client(string serverIP, int port, string name = "Anonymous")
        {   
            //Resolve the connection information
            IPAddress ip = IPAddress.Parse(serverIP);
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            this.Name = name;

            //Connect to server and start communication
            connectToServer(ip);
            startCommunication(endPoint);

        }

        private void connectToServer(IPAddress serverIP)
        {
            try
            {
                this.connection = new Socket(serverIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (SocketException se)
            {
                Console.WriteLine($"Caught SocketException: {se}");
                Console.WriteLine("Failed to connect to server, terminating client");
            }
            
        }

        private void startCommunication(IPEndPoint endPoint)
        {
            try
            {
                //Connecting to server
                connection.Connect(endPoint);
                Console.WriteLine($"Connection with {endPoint} established");

                //Starting the task to receive messages
                Task.Run(() => handleReceivedMessages());

                //Sending messages
                while (true)
                {
                    //Console.Write($"{this.Name}: ");
                    string decodedMessage = Console.ReadLine();
                    ClearLine();
                    //Console.Write($"\n");

                    byte[] encodedMessage = Encoding.ASCII.GetBytes(decodedMessage);
                    connection.Send(encodedMessage);
                    //Console.WriteLine($"{this.Name}: {decodedMessage}");
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine($"Caught SocketException: {se}");
                Console.WriteLine($"Connection terminated, killing client...");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Caught unhandled exception: {e}");
                Console.WriteLine($"Killing client...");
            }


        }

        private void handleReceivedMessages()
        {
            byte[] encodedMessage = new byte[1024];
            try
            {
                while (true)
                {
                    connection.Receive(encodedMessage);

                    string decodedMessage = Encoding.ASCII.GetString(encodedMessage);
                    Console.WriteLine($"Anonymous: {decodedMessage}");
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine($"Caught SocketException {se}");
                Console.WriteLine($"Connection timed out, killing client...");
                return;
            }
            
        }

        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop-1);
            Console.Write(new string(' ', Console.BufferWidth-1));
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        //private void createNewSocket()
        //{
        //    try
        //    {
        //        this.socket = new Socket(this.ServerIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}

        //private void connectToServer()
        //{
        //    try
        //    {
        //        //Establish connection
        //        socket.Connect(this.EndPoint);
        //        Console.WriteLine($"Connection with {this.EndPoint} established");

        //        //Send a message
        //        string msg = "This is a test<EOF>";
        //        sendMsg(msg);

        //        //Wait for response
        //        handleIncomingMessage();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}

        //private void handleIncomingMessage()
        //{
        //    try
        //    {
        //        //Receive encoded data from server;
        //        byte[] encodedMsg = new byte[1024];
        //        this.socket.Receive(encodedMsg);

        //        //Decode data
        //        string msg = Encoding.ASCII.GetString(encodedMsg);
        //    }
        //    catch(Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}

        //private void sendMsg(string msg)
        //{
        //    try
        //    {
        //        //Encode data
        //        byte[] encodedMsg = Encoding.ASCII.GetBytes(msg);
        //        this.socket.Send(encodedMsg);
        //        Console.WriteLine($"{this.Name}: {msg}");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}
    }
}
