using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.SignalR.Client;
using ZhangPengFei.IoT.MQ.Common;
using ZhangPengFei.IoT.RuleEngine;
using ZhangPengFei.IoT.RuleEngine.Core;
using ZhangPengFei.IoT.RuleEngine.RulePaser;

class Program
{
    private static HubConnection connection;

    static async Task Main(string[] args)
    {
        // 连接到 SignalR Hub
        connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5231/DataHub") // 替换为你的 SignalR Hub 地址
            .Build();

        // 监听连接状态变化
        connection.Closed += async (error) =>
        {
            Console.WriteLine("Connection closed. Trying to reconnect...");
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await connection.StartAsync();
        };

        // 开始连接
        await connection.StartAsync();

        // 当连接成功建立后订阅消息流
        connection.On<string, string>("SubscribeToTopic", async (topic, value) =>
        {
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
                Console.WriteLine(
                    $"Received message: [ Topic:{message.Topic} | Value:{message.Value} | Level:{message.Level} ] ");

                // 延迟加载规则引擎
                var lazyLoadRuleEngine = (NormalMessage message) => LazyLoadRuleEngine(message);


                // 将消息推送到 SignalR Hub
                await PushMessageToSignalRHub(message, lazyLoadRuleEngine);
            }
        });

        // 等待退出
        Console.WriteLine("Press Ctrl+C to exit.");
        await Task.Delay(-1);
    }

// 回调函数，用于将消息推送到 SignalR Hub，并执行延迟加载规则引擎的委托
    static async Task PushMessageToSignalRHub(NormalMessage message, Action<NormalMessage> ruleEngineDelegate)
    {
        // 执行延迟加载规则引擎的委托
        ruleEngineDelegate?.Invoke(message);

        // 将消息推送到 SignalR Hub
        await connection.InvokeAsync("SendMessage", message.Topic, message.Value);
    }

// 延迟加载规则引擎的方法
    static void LazyLoadRuleEngine(NormalMessage message)
    {
        new RuleEngineBuilder()
            .ConfigRulesLoader(new ConfigDefaultRuleLoader(message.Topic))
            .ConfigData(message.Value)
            .ConfigRulesParse(new RuleParser())
            .Build();
        Console.WriteLine("Lazy loading rule engine...");
    }
}