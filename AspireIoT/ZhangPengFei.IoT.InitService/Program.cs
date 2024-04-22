using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.SignalR.Client;
using ZhangPengFei.IoT.InitService.Common;
using ZhangPengFei.IoT.MQ.Common;

class Program
{
    static async Task Main(string[] args)
    {
        
        // 连接到 SignalR Hub
        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5231/DataHub") // 替换为你的 SignalR Hub 地址
            .Build();
        await connection.StartAsync();

        
        // gRPC 服务的地址
        var grpcAddress = "https://localhost:5029";

        // 创建 gRPC 通道
        using var channel = GrpcChannel.ForAddress(grpcAddress);

        // 创建订阅服务的客户端
        var client = new MessageService.MessageServiceClient(channel);

        // 构造订阅请求
        var request = new SubscriptionRequest
        {
            // 订阅所有IoT设备数据更新事件
            Topic = "/IoT/devices/+/data" 
        };

        // 订阅消息并获取流
        using var stream = client.SubscribeToTopic(request);

        // 接收并处理流中的消息
        while (await stream.ResponseStream.MoveNext())
        {
            var message = stream.ResponseStream.Current;
            Console.WriteLine($"Received message: [ Topic:{message.Topic} | Value:{message.Value} | Level:{message.Level} ] ");
            // 将消息推送到 SignalR Hub
            await connection.InvokeAsync("SendMessage", message.Topic, message.Value);
        }
        
        Console.WriteLine("Subscription completed.");
    }
}
