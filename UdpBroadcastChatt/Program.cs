using System.Net;
using System.Net.Sockets;
using System.Text;

class UdpBroadcastChat
{
    private const int ChatPort = 6000;

    static async Task Main()
    {
        Console.Title = "UDP Broadcast Chat";
        
        UdpClient udpChat = new UdpClient();
        
        udpChat.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        udpChat.Client.Bind(new IPEndPoint(IPAddress.Any, ChatPort));
        
        udpChat.EnableBroadcast = true;

        Console.WriteLine($"Chat started! Port: {ChatPort}. Write your super puper message:\n");
        
        _ = Task.Run(async () =>
        {
            while (true)
            {
                try
                {
                    var result = await udpChat.ReceiveAsync();
                    string message = Encoding.UTF8.GetString(result.Buffer);
                    
                    Console.WriteLine($"\n[{result.RemoteEndPoint}]: {message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка прийому: {ex.Message}");
                }
            }
        });
        
        while (true)
        {
            string text = Console.ReadLine();
            if (string.IsNullOrEmpty(text)) continue;

            byte[] bytes = Encoding.UTF8.GetBytes(text);
            
            IPEndPoint broadcastEP = new IPEndPoint(IPAddress.Broadcast, ChatPort);
            await udpChat.SendAsync(bytes, bytes.Length, broadcastEP);
        }
    }
}