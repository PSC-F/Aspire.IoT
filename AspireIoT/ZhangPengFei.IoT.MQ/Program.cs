using ZhangPengFei.IoT.MQ.MQTTExtension;
string broker = "127.0.0.1";
int port = 1883;
string clientId = Guid.NewGuid().ToString();
//string topic = "Csharp/mqtt";
string username = "";
string password = "";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc(); // ��� gRPC ��������

builder.Services.AddMqttClient(option =>
{
    option.WithTcpServer(broker, port);
    option.WithCredentials(username,password);
    option.WithClientId(clientId);
    option.WithCleanSession();
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseRouting(); // ��� UseRouting ����

app.MapGrpcService<MessageService>(); // ӳ�� gRPC ������

app.Run();