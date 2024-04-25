using System.Reflection;
using MySqlConnector;
using SqlSugar;
using ZhangPengFei.IoT.ApiService.Model;

namespace ZhangPengFei.IoT.ApiService.DataBase;

public class DataBaseManager(MySqlDataSource dataSource)
{
    private static int initialized = 0;

    /// <summary>
    /// 创建MySQL驱动
    /// </summary>
    /// <returns></returns>
    public SqlSugarClient BuildWithMySQL()
    {
        Console.WriteLine(dataSource.ConnectionString);
        // Server=localhost;Port=64375;User ID=root;Password=BE9zX5mx0ESwuPGx0RcvY};Database=mysqldb
        SqlSugarClient Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = dataSource.ConnectionString,
                DbType = DbType.MySql,
                IsAutoCloseConnection = false
            },
            db =>
            {
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    //获取原生SQL推荐 5.1.4.63  性能OK
                    // Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));

                    //获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
                    //Console.WriteLine(UtilMethods.GetSqlString(DbType.SqlServer,sql,pars))
                };
           
                if (Interlocked.CompareExchange(ref initialized, 1, 0) == 0)
                {
                    db.DbMaintenance.CreateDatabase();
                    Type[] types = typeof(GateWay).Assembly.GetTypes()
                        .Where(it => it.FullName.Contains("ZhangPengFei.IoT.ApiService.Model.")).ToArray();
                    db.CodeFirst.InitTables(types);
                }
            });


        return Db;
    }
}