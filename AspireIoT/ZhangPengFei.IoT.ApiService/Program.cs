using ZhangPengFei.IoT.ApiService.EndPoints.DevicesEndPoints;
using ZhangPengFei.IoT.ApiService.EndPoints.DevicesEndPoints.Services;

var builder = WebApplication.CreateSlimBuilder();
builder.Services.AddCors();
// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.Services.AddTransient<DeviceService>();
// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();
app.UseCors(
    builder => builder.WithOrigins("http://localhost:8848").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
// Configure the HTTP request pipeline.
app.UseExceptionHandler();
// 设备注册

app.MapDeviceRegisterEndPoint();

app.MapDefaultEndpoints();

app.Run();