using System.ComponentModel;
using SqlSugar;

namespace ZhangPengFei.IoT.ApiService.Model;

[SugarTable("Attribute")]
public class Attribute
{
    [SugarColumn(IsPrimaryKey = true)]
    [Description("属性标识")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Description("属性数据类型")] public string? Type { get; set; }
    [Description("属性名称")] public string? Name { get; set; }
    [Description("属性值")] public string? Value { get; set; }
}