using ReservationSystem.Models;

namespace ReservationSystem.Repositories
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetAllAsync();
        Task<Reservation?> GetByIdAsync(Guid id);
        Task AddAsync(Reservation reservation);
        void Update(Reservation reservation);
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
        List<Reservation> GetReservationsInDatePeriod(DateTime startDate, DateTime endDate);
    }
}
