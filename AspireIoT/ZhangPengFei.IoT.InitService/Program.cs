using Grpc.Core;
using Grpc.Net.Client;
using ZhangPengFei.IoT.InitService.Common;
using ZhangPengFei.IoT.MQ.Common;

class Program
{
    static async Task Main(string[] args)
    {
        // gRPC 服务的地址
        var grpcAddress = "https://localhost:7026";

        // 创建 gRPC 通道
        using var channel = GrpcChannel.ForAddress(grpcAddress);

        // 创建订阅服务的客户端
        var client = new MessageService.MessageServiceClient(channel);

        // 构造订阅请求
        var request = new SubscriptionRequest
        {
            Topic = "/IoT/#" 
        };

        // 订阅消息并获取流
        using var stream = client.SubscribeToTopic(request);

        // 接收并处理流中的消息
        while (await stream.ResponseStream.MoveNext())
        {
            var message = stream.ResponseStream.Current;
            Console.WriteLine($"Received message: [ Topic:{message.Topic} | Value:{message.Value} | Level:{message.Level} ] ");
        }
        
        Console.WriteLine("Subscription completed.");
    }
}
