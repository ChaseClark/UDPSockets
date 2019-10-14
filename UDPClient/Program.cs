using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            Console.WriteLine("Enter server IP address:");
            IPAddress ip = IPAddress.Parse(Console.ReadLine());

            Console.WriteLine("Enter port number:");
            int port = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Type your message, followed by the enter key...");

            //IPAddress ip = IPAddress.Parse("192.168.12.195");
            while (true)
            {
                byte[] message = Encoding.ASCII.GetBytes(Console.ReadLine());
                IPEndPoint ep = new IPEndPoint(ip, port);
                s.SendTo(message, ep);
                Console.WriteLine("Message sent to the server");
            }
        }
    }
}
