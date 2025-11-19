using ReservationSystem.Models;

namespace ReservationSystem.Repositories
{
    public interface ISysConstRepository
    {
        Task<SysConst> Get();
        Task Update(SysConst sysConst);

    }
}
