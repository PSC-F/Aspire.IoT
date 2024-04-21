using ZhangPengFei.IoT.MQ.MQTTExtension;
string broker = "127.0.0.1";
int port = 1883;
string clientId = Guid.NewGuid().ToString();
//string topic = "Csharp/mqtt";
string username = "";
string password = "";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc(); // 添加 gRPC 服务配置

builder.Services.AddMqttClient(option =>
{
    option.WithTcpServer(broker, port);
    option.WithCredentials(username,password);
    option.WithClientId(clientId);
    option.WithCleanSession();
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseRouting(); // 添加 UseRouting 方法

app.MapGrpcService<MessageService>(); // 映射 gRPC 服务类

app.Run();