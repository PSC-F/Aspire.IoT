using Grpc.Net.Client;
using ZhangPengFei.IoT.MQ.Common;

namespace ZhangPengFei.IoT.ApiService.EndPoints.DevicesEndPoints.Services;

public class DeviceService
{
    /// <summary>
    ///  注册设备
    /// </summary>
    /// <returns></returns>
    public bool RegisterDevice(out string deviceId)
    {
        deviceId = string.Empty;
        try
        {
            deviceId = GenerateDevicesId();
            var topic = $"/IoT/devices/{deviceId}/data";
            Console.WriteLine($"SUB {topic} ");
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
                Topic = topic
            };
            client.SubscribeToTopic(request);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    private string GenerateDevicesId()
    {
        return Guid.NewGuid().ToString();
    }
}