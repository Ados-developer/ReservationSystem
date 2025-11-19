using ReservationSystem.ViewModels;

namespace ReservationSystem.Services
{
    public interface ISysConstService
    {
        Task<SysConstViewModel> GetSysConstAsync();
        Task UpdateSysConst(SysConstViewModel model);
    }
}
