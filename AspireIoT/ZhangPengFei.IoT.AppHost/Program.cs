var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");

var mysqldb = builder.AddMySql("mysql")
    .WithPhpMyAdmin()
    .AddDatabase("IoTDB");

var apiService = builder.AddProject<Projects.ZhangPengFei_IoT_ApiService>("api-service")
    .WithReference(mysqldb)
    .WithReference(redis);
// ����mq
builder.AddProject<Projects.ZhangPengFei_IoT_MQ>("zhangpengfei-iot-mq-service");

builder.AddProject<Projects.ZhangPengFei_IoT_Hub>("zhangpengfei-iot-hub-service");

// builder.AddProject<Projects.ZhangPengFei_IoT_Web>("webfrontend")
//     .WithReference(redis)
//     .WithReference(apiService);
// ���������¼���
// builder.AddProject<Projects.ZhangPengFei_IoT_SSOService>("zhangpengfei-iot-sso-service")
//     .WithReference(redis);
// ��ʼ������
builder.AddProject<Projects.ZhangPengFei_IoT_InitGateWayService>("zhangpengfei-iot-initgateway-service");

builder.Build().Run();