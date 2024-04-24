using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System.Text;
using ZhangPengFei.IoT.MQ.Common;


public class MessageService : ZhangPengFei.IoT.MQ.Common.MessageService.MessageServiceBase
{
    private readonly IMqttClient _mqttClient;

    public MessageService(IMqttClient mqttClient)
    {
        _mqttClient = mqttClient;
    }

    public override async Task<PublishResponse> PublishMessage(NormalMessage request, ServerCallContext context)
    {
        try
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(request.Topic)
                .WithPayload(request.Value)
                .WithQualityOfServiceLevel((MqttQualityOfServiceLevel)request.Level)
                .WithRetainFlag()
                .Build();

            await _mqttClient.PublishAsync(message);

            // 这里编写发布消息的具体逻辑

            return new PublishResponse
            {
                Result = $"Message published successfully. TimeStamp: {request.Timestamp}",
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return new PublishResponse
        {
            Result = $"{request.Timestamp}",
        };
    }

    public override async Task SubscribeToTopic(SubscriptionRequest request,
        IServerStreamWriter<NormalMessage> responseStream, ServerCallContext context)
    {
        try
        {
            // 订阅 MQTT 主题
            await _mqttClient.SubscribeAsync(request.Topic);

            // 创建 CancellationTokenSource 对象用于取消订阅信号
            var cancellationTokenSource = new CancellationTokenSource();

            // 处理接收到的 MQTT 消息，并通过 gRPC 回调给客户端
            _mqttClient.ApplicationMessageReceivedAsync += async (e) =>
            {
                await responseStream.WriteAsync(new NormalMessage
                {
                    Topic = e.ApplicationMessage.Topic,
                    Value = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment),
                    Level = (int)e.ApplicationMessage.QualityOfServiceLevel,
                });
            };


            // 等待取消订阅信号
            await Task.Run(() => { context.CancellationToken.WaitHandle.WaitOne(); });
            // 取消订阅 MQTT 主题
            await _mqttClient.UnsubscribeAsync(request.Topic);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}