using ReservationSystem.Mappers;
using ReservationSystem.Models;
using ReservationSystem.Repositories;
using ReservationSystem.ViewModels;

namespace ReservationSystem.Services
{
    public class SysConstService : ISysConstService
    {
        private readonly ISysConstRepository _repository;
        public SysConstService(ISysConstRepository repository)
        {
            _repository = repository;
        }
        public async Task<SysConstViewModel> GetSysConstAsync()
        {
            SysConst? sysConst = await _repository.Get();
            return SysConstMapper.ToViewModel(sysConst);
        }
        public async Task UpdateSysConst(SysConstViewModel viewModel)
        {
            SysConst? model = SysConstMapper.ToModel(viewModel);
            await _repository.Update(model);
        }
    }
}
