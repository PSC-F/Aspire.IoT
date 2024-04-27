using System.ComponentModel;
using SqlSugar;


namespace ZhangPengFei.IoT.ApiService.Model;

[SugarTable("Event")]
public class Event
{
    [SugarColumn(IsPrimaryKey = true)]
    [Description("事件标识")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Description("事件名称")] public string? Name { get; set; }
    [Description("触发时间")] public DateTime? Timestamp { get; set; }

    [SugarColumn(IsIgnore = true)]
    [Description("关联属性标识")]
    public string? AttributeId { get; set; } // 存储用户选择的属性标识
    [SugarColumn(IsIgnore = true)]
    [Description("关联命令标识")] public string? CommandId { get; set; } // 存储用户选择的命令标识
}