using System.Net;
using System.Net.Sockets;
using System.Text;

class EchoUdpClient
{
    static async Task Main()
    {
        UdpClient client = new UdpClient();
        
        IPEndPoint[] servers = new IPEndPoint[]
        {
            new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5005),
            new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5006)  
        };

        Console.WriteLine("Enter a message for the server: \n");
        string message = Console.ReadLine();
        if (string.IsNullOrEmpty(message)) return;
        
        byte[] bytes = Encoding.UTF8.GetBytes(message);

        foreach (var server in servers)
        {
            await client.SendAsync(bytes, bytes.Length, server);
            Console.WriteLine($"Message was sent to server: {server}");
        }

        Console.WriteLine("Waiting for respose....");

        try
        {
            var receiveResult = await client.ReceiveAsync();
            
            var responseText = Encoding.UTF8.GetString(receiveResult.Buffer);

            Console.WriteLine($"Receive Message from {receiveResult.RemoteEndPoint}:{responseText}");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error receiving {ex.Message}");
        }

        Console.ReadKey();

    }
}