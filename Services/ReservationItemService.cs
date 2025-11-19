using ReservationSystem.Mappers;
using ReservationSystem.Models;
using ReservationSystem.Repositories;
using ReservationSystem.ViewModels;

namespace ReservationSystem.Services
{
    public class ReservationItemService : IReservationItemService
    {
        private readonly IReservationItemRepository _repository;
        public ReservationItemService(IReservationItemRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<ReservationItemViewModel>> GetAllAsync()
        {
            List<ReservationItem> items = await _repository.GetAllAsync();
            return items.Select(item => ReservationItemMapper.ToViewModel(item)).ToList();
        }
        public async Task<ReservationItemViewModel?> GetByIdAsync(Guid id)
        {
            ReservationItem? item = await _repository.GetByIdAsync(id);
            return item == null ? null : ReservationItemMapper.ToViewModel(item);
        }
        public async Task CreateAsync(ReservationItemViewModel itemViewModel)
        {
            ReservationItem item = ReservationItemMapper.ToModel(itemViewModel);
            await _repository.AddAsync(item);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateAsync(ReservationItemViewModel itemViewModel)
        {
            ReservationItem item = ReservationItemMapper.ToModel(itemViewModel);
            _repository.Update(item);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
        }
    }
}
