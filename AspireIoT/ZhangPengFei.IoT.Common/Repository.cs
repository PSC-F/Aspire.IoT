using SqlSugar;

namespace ZhangPengFei.IoT.Common;

public class Repository<T>: SimpleClient<T> where T : class, new()
{
    public Repository(ISqlSugarClient db)
    {          
        base.Context=db; 
    }
}