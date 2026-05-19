using System.Net;
using System.Net.Sockets;
using System.Text;

class SimpleUdpServer
{
    static void Main()
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 5005);
        socket.Bind(endPoint);

        while (true)
        {
            byte[] buffer = new byte[1024];
           
            EndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            int bytesRecieved = socket.ReceiveFrom(buffer, ref sender);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRecieved);
            Console.WriteLine($"{sender} : {message}");

            string responseText = $"Hi,your message arrived!Length: {bytesRecieved} bait\n";
            byte[] messageBytes = Encoding.UTF8.GetBytes(responseText);

            socket.SendTo(messageBytes, sender);
        }
    }
}