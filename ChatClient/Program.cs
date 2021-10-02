using System;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client("127.0.0.1", 12345);
        }
    }
}
