var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.ZhangPengFei_IoT_ApiService>("apiservice");
// 启动mq
builder.AddProject<Projects.ZhangPengFei_IoT_MQ>("zhangpengfei-iot-mqservice");

builder.AddProject<Projects.ZhangPengFei_IoT_Web>("webfrontend")
    .WithReference(cache)
    .WithReference(apiService);
// 启动单点登录组件
builder.AddProject<Projects.ZhangPengFei_IoT_SSOService>("zhangpengfei-iot-ssoservice")
     .WithReference(cache);
// 初始化网关
builder.AddProject<Projects.ZhangPengFei_IoT_InitGateWayService>("zhangpengfei-iot-initgatewayservice");

builder.Build().Run();
