using MySqlConnector;
using SqlSugar;
using ZhangPengFei.IoT.ApiService.DataBase;
using ZhangPengFei.IoT.ApiService.Model;

namespace ZhangPengFei.IoT.ApiService.EndPoints.DevicesEndPoints.Services;

public class DeviceService(MySqlDataSource dataSource)
{
    private SimpleClient<Device> _repository =
        new DataBaseManager(dataSource).BuildWithMySQL().GetSimpleClient<Device>();

    public async Task<bool> AddDeviceAsync(Device device)
    {
        return await _repository.InsertOrUpdateAsync(device);
    }

    public async Task<List<Device>> ListDeviceAsync()
    {
        return await _repository.GetListAsync();
    }

    public async Task<bool> DeleteDeviceAsync(Device device)
    {
        return await _repository.DeleteAsync(device);
    }
}