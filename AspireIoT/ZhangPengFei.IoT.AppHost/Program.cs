var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.ZhangPengFei_IoT_ApiService>("api-service");
// ����mq
builder.AddProject<Projects.ZhangPengFei_IoT_MQ>("zhangpengfei-iot-mq-service");

builder.AddProject<Projects.ZhangPengFei_IoT_Hub>("zhangpengfei-iot-hub-service");

builder.AddProject<Projects.ZhangPengFei_IoT_Web>("webfrontend")
    .WithReference(cache)
    .WithReference(apiService);
// ���������¼���
builder.AddProject<Projects.ZhangPengFei_IoT_SSOService>("zhangpengfei-iot-sso-service")
     .WithReference(cache);
// ��ʼ������
builder.AddProject<Projects.ZhangPengFei_IoT_InitGateWayService>("zhangpengfei-iot-initgateway-service");

builder.Build().Run();
