using ReservationSystem.Models;

namespace ReservationSystem.Repositories
{
    public interface IReservationItemRepository
    {
        Task<List<ReservationItem>> GetAllAsync();
        Task<ReservationItem?> GetByIdAsync(Guid id);
        Task AddAsync(ReservationItem reservationItem);
        void Update(ReservationItem reservationItem);
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
    }
}
