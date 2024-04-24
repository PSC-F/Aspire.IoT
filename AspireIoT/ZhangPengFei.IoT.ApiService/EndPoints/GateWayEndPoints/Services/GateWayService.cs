using MySqlConnector;
using SqlSugar;
using ZhangPengFei.IoT.ApiService.DataBase;
using ZhangPengFei.IoT.ApiService.EndPoints.GateWayEndPoints.Model;


namespace ZhangPengFei.IoT.ApiService.EndPoints.GateWayEndPoints.Services;

public class GateWayService(MySqlDataSource dataSource)
{
    private SimpleClient<GateWay> _gateRepository =
        new DataBaseManager(dataSource).BuildWithMySQL().GetSimpleClient<Model.GateWay>();

    public async Task<bool> AddGateWayAsync(Model.GateWay gateWay)
    {
        return await _gateRepository.InsertOrUpdateAsync(gateWay);
    }

    public async Task<List<Model.GateWay>> ListGateWayAsync()
    {
        return await _gateRepository.GetListAsync();
    }

    public async Task<bool> DeleteGateWayAsync(Model.GateWay gateWay)
    {
        return await _gateRepository.DeleteAsync(gateWay);
    }
}