using Microsoft.AspNetCore.Mvc;
using ZhangPengFei.IoT.ApiService.EndPoints.GateWayEndPoints.Services;
using ZhangPengFei.IoT.Common.Model.GateWay;

namespace ZhangPengFei.IoT.ApiService.EndPoints.GateWayEndPoints;

public static class GateWayEndPoint
{
    public static void MapAddGateWayEndPoint(this WebApplication app)
    {
        var api = app.MapGroup("/api");
        var productApi = api.MapGroup("/gateway").WithGroupName("添加网关");
        productApi.MapPost("/add", async (GateWayService service, [FromBody] GateWay gateWay) =>
        {
            if (await service.AddGateWayAsync(gateWay))
            {
                return Results.Json(new { GateWayId = gateWay.Id });
            }

            return Results.BadRequest();
        });
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
            async (GateWayService service) => Results.Json(new { data = await service.ListGateWayAsync() }));
    }
}