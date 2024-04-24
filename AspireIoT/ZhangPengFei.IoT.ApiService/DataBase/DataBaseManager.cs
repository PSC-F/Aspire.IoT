using MySqlConnector;
using SqlSugar;


namespace ZhangPengFei.IoT.ApiService.DataBase;

public class DataBaseManager(MySqlDataSource dataSource)
{
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
                IsAutoCloseConnection = true
            },
            db =>
            {
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    //获取原生SQL推荐 5.1.4.63  性能OK
                    Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));

                    //获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
                    //Console.WriteLine(UtilMethods.GetSqlString(DbType.SqlServer,sql,pars))
                };
                //注意多租户 有几个设置几个
                //db.GetConnection(i).Aop
            });
        Db.DbMaintenance.CreateDatabase();
        Db.CodeFirst.InitTables<EndPoints.GateWayEndPoints.Model.GateWay>();
        return Db;
    }
}