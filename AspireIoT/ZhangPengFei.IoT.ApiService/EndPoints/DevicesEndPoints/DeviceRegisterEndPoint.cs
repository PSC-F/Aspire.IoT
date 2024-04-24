using ZhangPengFei.IoT.ApiService.EndPoints.DevicesEndPoints.Services;

namespace ZhangPengFei.IoT.ApiService.EndPoints.DevicesEndPoints;

public static class DeviceRegisterEndPoint
{
    /// <summary>
    /// 动态订阅
    /// </summary>
    /// <param name="app"></param>
    // public static void MapDeviceRegisterEndPoint(this WebApplication app)
    // {
    //     var api = app.MapGroup("/api");
    //     var productApi = api.MapGroup("/device").WithGroupName("设备注册");
    //     productApi.MapGet("/register", (DeviceService service) =>
    //     {
    //         if (service.RegisterDevice(out string deviceId))
    //         {
    //             return Results.Json(new { DeviceId = deviceId });
    //         }
    //
    //         return Results.BadRequest();
    //     });
    // }
}