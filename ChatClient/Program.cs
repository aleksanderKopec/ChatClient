using System;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 12345;
            if (args.Length == 2)
            {
                if (isIpValid(args[0])) { ip = args[0]; }
                else { Console.WriteLine("Please enter a valid ip adress"); return; }

                int intPortArg = Int32.Parse(args[1]);
                if (isPortValid(intPortArg)) { port = intPortArg; }
                else { Console.WriteLine("Please enter a valid port"); return; }
            }

            Console.WriteLine($"Starting chat client with ServerIp: {ip} and port: {port}");
            Client client = new Client(ip, port);
        }

        static bool isIpValid(string ip)
        {
            ip.Trim();
            string[] ipArray = ip.Split('.');

            foreach (string octet in ipArray)
            {
                int intOctet = Int32.Parse(octet);
                if (intOctet < 0 || intOctet > 255)
                {
                    return false;
                }
            }
            return true;
        }

        static bool isPortValid(int port)
        {
            if (port < 0 || port > 65535)
            {
                return false;
            }
            return true;
        }
    }
}
