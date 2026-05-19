using System.Net;
using System.Net.Sockets;
using System.Text;

class EchoUdpServer
{
    static async Task Main()
    {
        UdpClient server = new UdpClient(5005);
        
        
        while (true)  
        {
            UdpReceiveResult senderResult = await server.ReceiveAsync();
            byte[] bytes = senderResult.Buffer;
            IPEndPoint sender = senderResult.RemoteEndPoint;
            
            string message = Encoding.UTF8.GetString(bytes);
            Console.WriteLine($"Get a message from {sender}:{message}");

            string clientMessage = $"The message was gain by server.Bytes received: {bytes.Length} bytes";
            byte[] getBytes = Encoding.UTF8.GetBytes(clientMessage);
            
            await server.SendAsync(getBytes, getBytes.Length,sender);   
            

        }
    }
}