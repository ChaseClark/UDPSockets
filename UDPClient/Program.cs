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
            try
            {
                while (true)
                {
                    byte[] message = Encoding.ASCII.GetBytes($"{Console.ReadLine()}");
                    IPEndPoint ep = new IPEndPoint(ip, port);
                    s.SendTo(message, ep);
                    Console.WriteLine("Message sent to the server");
                    byte[] replyBytes = new byte[256];
                    s.Receive(replyBytes);
                    Console.WriteLine(Encoding.ASCII.GetString(replyBytes, 0, replyBytes.Length));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
