using System.ComponentModel;
using SqlSugar;

namespace ZhangPengFei.IoT.ApiService.Model;

[SugarTable("GateWay")] //当和数据库名称不一样可以设置表别名 指定表明
public class GateWay
{
    [Description("网关编号")]
    [SugarColumn(IsPrimaryKey = true)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Description("网关名称")] public string? Name { get; set; }
    [Description("网关类型")] public string? Type { get; set; }
    [Description("网关状态")] public bool? State { get; set; }
    [Description("网关描述")] public string? Desc { get; set; }
    [Description("网关备注")] public string? Remark { get; set; }

    [Description("创建时间")]
    [SugarColumn(InsertServerTime = true)]
    public DateTime? DateTime { get; set; }

    [Description("设备列表")]
    [SugarColumn(IsIgnore = true)]
    public List<Device>? Devices { get; set; }
}