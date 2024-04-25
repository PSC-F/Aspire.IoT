using System.ComponentModel;
using SqlSugar;

namespace ZhangPengFei.IoT.ApiService.Model;

[SugarTable("Service")]
public class Service
{
    [Description("服务标识")]
    [SugarColumn(IsPrimaryKey = true)]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Description("服务名称")]
    public string? ServiceName { get; set; }
    [Description("属性列表")]
    [SugarColumn(IsIgnore = true)]
    public List<Attribute>? Attributes { get; set; }

    [Description("命令列表")]
    [SugarColumn(IsIgnore = true)]
    public List<Command>? Commands{ get; set; }

    [Description("事件列表")]
    [SugarColumn(IsIgnore = true)]
    public List<Event>? Events { get; set; }
    
}