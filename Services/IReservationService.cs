using ReservationSystem.Models;
using ReservationSystem.ViewModels;

namespace ReservationSystem.Services
{
    public interface IReservationService
    {
        Task<List<ReservationViewModel>> GetAllAsync();
        Task<ReservationViewModel?> GetByIdAsync(Guid id);
        Task CreateAsync(ReservationViewModel reservationViewModel);
        Task UpdateAsync(ReservationViewModel reservationViewModel);
        Task DeleteAsync(Guid id);
    }
}
