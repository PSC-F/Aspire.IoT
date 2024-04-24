using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZhangPengFei.IoT.ApiService.EndPoints.GateWayEndPoints.Model;
using StackExchange.Redis;
using ZhangPengFei.IoT.ApiService.EndPoints.GateWayEndPoints.Services;


namespace ZhangPengFei.IoT.ApiService.EndPoints.GateWayEndPoints;

public static class GateWayEndPoint
{
    public static void MapAddGateWayEndPoint(this WebApplication app)
    {
        try
        {
            var api = app.MapGroup("/api");
            var productApi = api.MapGroup("/gateway").WithGroupName("添加网关");
            productApi.MapPost("/add",
                async (GateWayService service, IConnectionMultiplexer redis,
                    [FromBody] GateWay gateWay) =>
                {
                    if (await service.AddGateWayAsync(gateWay))
                    {
                        await redis.GetDatabase().SetAddAsync((RedisKey)"GateWaySet",
                            System.Text.Json.JsonSerializer.Serialize(gateWay));
                        return Results.Json(new { GateWayId = gateWay.Id });
                    }

                    return Results.BadRequest();
                });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static void MapDeleteGateWayEndPoint(this WebApplication app)
    {
        var api = app.MapGroup("/api");
        var productApi = api.MapGroup("/gateway").WithGroupName("删除网关");
        productApi.MapPost("/delete", async (GateWayService service, [FromBody] GateWay gateWay) =>
        {
            if (await service.DeleteGateWayAsync(gateWay))
            {
                return Results.Json(new { GateWayId = gateWay.Id });
            }

            return Results.BadRequest();
        });
    }

    public static void MapGetGateWaysEndPoint(this WebApplication app)
    {
        var api = app.MapGroup("/api");
        var productApi = api.MapGroup("/gateway").WithGroupName("获取网关列表");
        productApi.MapGet("/list",
            async (GateWayService service, IConnectionMultiplexer redis) =>
            {
                if (await redis.GetDatabase().SetLengthAsync((RedisKey)"GateWaySet") > 0)
                {
                    List<GateWay?> gateways = (await redis.GetDatabase().SetMembersAsync((RedisKey)"GateWaySet"))
                        .Select(member => System.Text.Json.JsonSerializer.Deserialize<GateWay>(member.ToString()))
                        .ToList();
                    return Results.Json(
                        new { data = gateways });
                }

                return Results.Json(new { data = await service.ListGateWayAsync() });
            });
    }
}