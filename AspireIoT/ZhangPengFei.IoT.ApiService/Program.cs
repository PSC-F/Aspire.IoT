using Microsoft.OpenApi.Models;
using ZhangPengFei.IoT.ApiService.EndPoints;
using ZhangPengFei.IoT.ApiService.EndPoints.DevicesEndPoints.Services;
using ZhangPengFei.IoT.ApiService.EndPoints.GateWayEndPoints;
using ZhangPengFei.IoT.ApiService.EndPoints.GateWayEndPoints.Services;
using Microsoft.AspNetCore.OpenApi;
using MySqlConnector;
using SqlSugar;
using ZhangPengFei.IoT.ApiService.DataBase;

var builder = WebApplication.CreateBuilder();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// 添加 Swagger 服务

builder.AddServiceDefaults();


// add EndPointService
builder.Services.AddTransient<GateWayService>();
builder.Services.AddTransient<DeviceService>();

builder.AddMySqlDataSource("IoTDB");
builder.AddRedisDistributedCache("redis");
builder.AddRedisClient("redis");
// add cor
builder.Services.AddCors();
// Add service defaults & Aspire components.

// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();
// 启用 Swagger 中间件

app.UseSwagger();
app.UseSwaggerUI();


app.UseCors(
    builder => builder.WithOrigins("http://localhost:8848").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
// Configure the HTTP request pipeline.
app.UseExceptionHandler();
// 【网关】
// 添加网关
app.MapAddGateWayEndPoint();
// 删除网关
app.MapDeleteGateWayEndPoint();
// 获取网关列表
app.MapGetGateWaysEndPoint();
// 【设备】
// 添加设备
app.MapAddDeviceEndPoint();
// 删除设备
app.MapDeleteDeviceEndPoint();
// 获取设备列表
app.MapGetDevicesEndPoint();

//
app.MapDefaultEndpoints();


app.Run();