using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer
{
    class Program
    {
        // add comments
        private const int port = 12345;
        static void Main(string[] args)
        {
            UdpClient listener = new UdpClient(port);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, port);
            int count = 1;

            try
            {
                Console.WriteLine("My IP is... ");
                PrintIPAddress();
                Console.WriteLine();
                Console.WriteLine($"My port is {port}");
                Console.WriteLine();
                Console.WriteLine();
                while (true)
                {
                    Console.WriteLine("Waiting for client message...");
                    byte[] bytes = listener.Receive(ref groupEP);
                    string rec = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

                    // print out received message
                    Console.WriteLine($"Received message from {groupEP} :");
                    Console.WriteLine($" {rec}");

                    // send a response to the client
                    byte[] replyMessage = Encoding.ASCII.GetBytes($"(Server) I have received {count} message(s).");
                    listener.Send(replyMessage,replyMessage.Length, groupEP);
                    count++;

                    // blank line
                    Console.WriteLine();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }
        }

        // Gets all ipv4 connections on this computer
        private static void PrintIPAddress()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    Console.WriteLine(ni.Name);
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            Console.WriteLine(ip.Address.ToString());
                        }
                    }
                }
            }
        }
    }
}
