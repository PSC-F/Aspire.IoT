using System.Text.Json;
using ZhangPengFei.IoT.ApiService.EndPoints.DevicesEndPoints.Services;

namespace ZhangPengFei.IoT.ApiService.EndPoints.DevicesEndPoints;

public static class DeviceRegisterEndPoint
{
    public static void MapDeviceRegisterEndPoint(this WebApplication app)
    {
        var api = app.MapGroup("/api");
        var productApi = api.MapGroup("/device").WithGroupName("设备注册");
        productApi.MapGet("/Register", (DeviceService service) =>
        {
            if (service.RegisterDevice())
            {
                return Results.Ok();
            }
            return Results.BadRequest();
        });
    }
}