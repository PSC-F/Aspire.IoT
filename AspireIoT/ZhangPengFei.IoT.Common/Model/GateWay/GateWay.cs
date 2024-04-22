using SqlSugar;

namespace ZhangPengFei.IoT.Common.Model.GateWay;

[SugarTable("DBGateWay")] //当和数据库名称不一样可以设置表别名 指定表明
public class GateWay
{
    [SugarColumn(IsPrimaryKey = true)]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string Type { get; set; }
    public bool State { get; set; }
    public string Desc { get; set; }
    public string Remark { get; set; }
}