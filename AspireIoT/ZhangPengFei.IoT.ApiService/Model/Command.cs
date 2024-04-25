using System.ComponentModel;
using SqlSugar;

namespace ZhangPengFei.IoT.ApiService.Model;

[SugarTable("Command")]
public class Command
{
    [SugarColumn(IsPrimaryKey = true)]
    [Description("命令标识")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Description("命令名称")] public string? Name { get; set; }
    [Description("命令说明")] public string? Desc { get; set; }
    [Description("命令")] public string? Execute { get; set; }
}