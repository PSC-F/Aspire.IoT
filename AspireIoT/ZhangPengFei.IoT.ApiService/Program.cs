using ZhangPengFei.IoT.ApiService.EndPoints.DevicesEndPoints;
using ZhangPengFei.IoT.ApiService.EndPoints.DevicesEndPoints.Services;
using ZhangPengFei.IoT.ApiService.EndPoints.GateWayEndPoints;
using ZhangPengFei.IoT.ApiService.EndPoints.GateWayEndPoints.Services;
using ZhangPengFei.IoT.Common;

var builder = WebApplication.CreateSlimBuilder();
// 注入仓储
builder.Services.AddScoped(typeof(Repository<>));
builder.AddServiceDefaults();
builder.Services.AddTransient<DeviceService>();
builder.Services.AddTransient<GateWayService>();


// add cor
builder.Services.AddCors();
// Add service defaults & Aspire components.

// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();
app.UseCors(
    builder => builder.WithOrigins("http://localhost:8848").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
// Configure the HTTP request pipeline.
app.UseExceptionHandler();
// 设备注册 [废弃]
app.MapDeviceRegisterEndPoint();
// 添加网关
app.MapAddGateWayEndPoint();
// 删除网关
app.MapDeleteGateWayEndPoint();
// 获取网关列表
app.MapGetGateWaysEndPoint();

app.MapDefaultEndpoints();

app.Run();