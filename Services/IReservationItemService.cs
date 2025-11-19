using ReservationSystem.Models;
using ReservationSystem.ViewModels;

namespace ReservationSystem.Services
{
    public interface IReservationItemService
    {
        Task<List<ReservationItemViewModel>> GetAllAsync();
        Task<ReservationItemViewModel?> GetByIdAsync(Guid id);
        Task CreateAsync(ReservationItemViewModel reservationItemViewModel);
        Task UpdateAsync(ReservationItemViewModel reservationItemViewModel);
        Task DeleteAsync(Guid id);
    }
}
