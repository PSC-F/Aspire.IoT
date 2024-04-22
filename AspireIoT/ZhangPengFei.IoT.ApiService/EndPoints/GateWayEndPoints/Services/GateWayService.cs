using ZhangPengFei.IoT.Common;
using ZhangPengFei.IoT.Common.Model.GateWay;

namespace ZhangPengFei.IoT.ApiService.EndPoints.GateWayEndPoints.Services;

public class GateWayService(Repository<GateWay> gateRepository)
{
    Repository<GateWay> _gateRepository = gateRepository;

    public async Task<bool> AddGateWayAsync(GateWay gateWay)
    {
        return await _gateRepository.InsertOrUpdateAsync(gateWay);
    }

    public async Task<List<GateWay>> ListGateWayAsync()
    {
        return await _gateRepository.GetListAsync();
    }

    public async Task<bool> DeleteGateWayAsync(GateWay gateWay)
    {
        return await _gateRepository.DeleteAsync(gateWay);
    }
}