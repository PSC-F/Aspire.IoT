var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.ZhangPengFei_IoT_ApiService>("apiservice");
// ����mq
builder.AddProject<Projects.ZhangPengFei_IoT_MQ>("zhangpengfei-iot-mqservice");

builder.AddProject<Projects.ZhangPengFei_IoT_Web>("webfrontend")
    .WithReference(cache)
    .WithReference(apiService);
// ���������¼���
builder.AddProject<Projects.ZhangPengFei_IoT_SSOService>("zhangpengfei-iot-ssoservice")
     .WithReference(cache);
// ��ʼ������
builder.AddProject<Projects.ZhangPengFei_IoT_InitGateWayService>("zhangpengfei-iot-initgatewayservice");

builder.Build().Run();
