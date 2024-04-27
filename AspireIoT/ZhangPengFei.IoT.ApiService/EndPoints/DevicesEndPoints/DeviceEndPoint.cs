using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using ZhangPengFei.IoT.ApiService.EndPoints.DevicesEndPoints.Services;
using ZhangPengFei.IoT.ApiService.Model;

namespace ZhangPengFei.IoT.ApiService.EndPoints;

public static class DeviceEndPoint
{
    public static void MapAddDeviceEndPoint(this WebApplication app)
    {
        try
        {
            var api = app.MapGroup("/api");
            var productApi = api.MapGroup("/device").WithGroupName("添加设备");
            productApi.MapPost("/add",
                async (DeviceService service, IConnectionMultiplexer redis,
                    [FromBody] Device device) =>
                {
                    if (await service.AddDeviceAsync(device))
                    {
                        await redis.GetDatabase().SetAddAsync((RedisKey)"DevicesSet",
                            System.Text.Json.JsonSerializer.Serialize(device));
                        return Results.Json(new { data = device.Id });
                    }

                    return Results.BadRequest();
                }).WithOpenApi();
            ;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static void MapDeleteDeviceEndPoint(this WebApplication app)
    {
        var api = app.MapGroup("/api");
        var productApi = api.MapGroup("/device").WithGroupName("删除网关");
        productApi.MapPost("/delete",
            async (DeviceService service, IConnectionMultiplexer redis, [FromBody] Device device) =>
            {
                if (await service.DeleteDeviceAsync(device))
                {
                    await redis.GetDatabase()
                        .SetRemoveAsync((RedisKey)"DevicesSet", System.Text.Json.JsonSerializer.Serialize(device));
                    return Results.Json(new { data = device.Id });
                }

                return Results.BadRequest();
            }).WithOpenApi();
        ;
    }

    public static void MapGetDevicesEndPoint(this WebApplication app)
    {
        var api = app.MapGroup("/api");
        var productApi = api.MapGroup("/device").WithGroupName("获取设备列表");
        productApi.MapGet("/list",
            async (DeviceService service, IConnectionMultiplexer redis) =>
            {
                if (await redis.GetDatabase().SetLengthAsync((RedisKey)"DevicesSet") > 0)
                {
                    List<Device?> devices = (await redis.GetDatabase().SetMembersAsync((RedisKey)"DevicesSet"))
                        .Select(member => System.Text.Json.JsonSerializer.Deserialize<Device>(member.ToString()))
                        .ToList();
                    return Results.Json(
                        new { data = devices });
                }

                return Results.Json(new { data = await service.ListDeviceAsync() });
            }).WithOpenApi();
        ;
    }
}