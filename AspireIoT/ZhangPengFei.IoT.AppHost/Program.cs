var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");

var mysqldb = builder.AddMySql("mysql")
    .WithPhpMyAdmin()
    .AddDatabase("IoTDB");

var apiService = builder.AddProject<Projects.ZhangPengFei_IoT_ApiService>("api-service")
    .WithReference(mysqldb)
    .WithReference(redis);
// 启动mq
builder.AddProject<Projects.ZhangPengFei_IoT_MQ>("zhangpengfei-iot-mq-service");

builder.AddProject<Projects.ZhangPengFei_IoT_Hub>("zhangpengfei-iot-hub-service");

// builder.AddProject<Projects.ZhangPengFei_IoT_Web>("webfrontend")
//     .WithReference(redis)
//     .WithReference(apiService);
// 启动单点登录组件
// builder.AddProject<Projects.ZhangPengFei_IoT_SSOService>("zhangpengfei-iot-sso-service")
//     .WithReference(redis);
// 初始化网关
builder.AddProject<Projects.ZhangPengFei_IoT_InitGateWayService>("zhangpengfei-iot-initgateway-service");

builder.Build().Run();