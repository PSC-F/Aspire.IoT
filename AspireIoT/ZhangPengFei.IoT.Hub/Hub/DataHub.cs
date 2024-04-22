using Microsoft.AspNetCore.SignalR;

namespace ZhangPengFei.IoT.Hub.Hub;

public class DataHub: Microsoft.AspNetCore.SignalR.Hub
{
    // 这里可以定义处理客户端连接和消息的方法
    // 例如，处理客户端发送的消息并将消息广播给所有连接的客户端
    public async Task SendMessage(string topic, string message)
    {
        Console.WriteLine(topic);
        Console.WriteLine(message);
        await Clients.All.SendAsync("ReceiveMessage", topic, message);
    }
    
}