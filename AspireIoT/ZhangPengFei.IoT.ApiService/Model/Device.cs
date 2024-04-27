using System.ComponentModel;
using SqlSugar;

namespace ZhangPengFei.IoT.ApiService.Model;

[SugarTable("Device")]
public class Device
{
    [SugarColumn(IsPrimaryKey = true)]
    [Description("设备标识")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Description("设备名称")] public string? Name { get; set; }
    [Description("设备类型")] public string? Type { get; set; }
    [Description("设备状态")] public string? State { get; set; }
    [SugarColumn(IsIgnore = true)]
    [Description("服务列表")] public List<Service>? Services { get; set; }
}