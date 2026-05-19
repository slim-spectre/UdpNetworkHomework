using System.Net;
using System.Net.Sockets;
using System.Text;

class SimpleUdpClient
{
    static void Main()
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        socket.ReceiveTimeout = 2000;
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 5005);
        
        Console.WriteLine("Enter your message: ");
        var message = Console.ReadLine();

        if (string.IsNullOrEmpty(message)) return;
        byte[] sendData = Encoding.UTF8.GetBytes(message);
        socket.SendTo(sendData, endPoint);
        
        byte[] receiveData = new byte[1024];
        EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        try
        {
            int bytesReceived = socket.ReceiveFrom(receiveData, ref remoteEndPoint);

            string response = Encoding.UTF8.GetString(receiveData, 0, bytesReceived);
            Console.WriteLine($"Response from the server {remoteEndPoint}: " + response);
        }
        catch (SocketException e) when (e.SocketErrorCode == SocketError.TimedOut)
        {
            Console.WriteLine("Timed out");
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
            
        }

        Console.ReadKey();


    }
}