using MQTTnet;
using MQTTnet.Client;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace ZhangPengFei.IoT.MQ.MQTTExtension
{
    public static class MQTTExtension
    {
        public static void AddMqttClient(this IServiceCollection services, Action<MqttClientOptionsBuilder> configure)
        {
            try
            {
                // 创建 MQTT 客户端选项构建器
                var optionsBuilder = new MqttClientOptionsBuilder();

                // 调用配置方法，允许用户自定义选项
                configure?.Invoke(optionsBuilder);

                // 构建 MQTT 客户端选项
                var options = optionsBuilder.Build();

                // 注册 MQTT 客户端实例
                services.AddSingleton<IMqttClient>(provider =>
                {
                    var factory = new MqttFactory();
                    var client = factory.CreateMqttClient();
                    client.ConnectAsync(options).GetAwaiter().GetResult();
                    return client;
                });

                // 注册 MQTT 客户端连接选项
                services.AddSingleton(options);
            }
            catch (Exception ex)
            {

                Console.WriteLine (ex); 
            }
            
        }
    }
}
