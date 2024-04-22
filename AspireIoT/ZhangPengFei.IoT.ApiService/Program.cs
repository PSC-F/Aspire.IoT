using ZhangPengFei.IoT.ApiService.EndPoints.DevicesEndPoints;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

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