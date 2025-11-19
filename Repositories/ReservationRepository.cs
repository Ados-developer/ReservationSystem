using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Models;

namespace ReservationSystem.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ReservationSystemDbContext _context;
        public ReservationRepository(ReservationSystemDbContext context)
        {
            _context = context;
        }
        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _context.Reservations
                .Include(r => r.ReservationItem)
                .ToListAsync();
        }
        public async Task<Reservation?> GetByIdAsync(Guid id)
        {
            return await _context.Reservations
                .Include(r => r.ReservationItem)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
        }
        public void Update(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
        }
        public async Task DeleteAsync(Guid id)
        {
            Reservation? reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public List<Reservation> GetReservationsInDatePeriod(DateTime startDate, DateTime endDate)
        {
            return _context.Reservations
                .Include(r => r.ReservationItem) // aby sa načítala aj dĺžka služby
                .Where(r => r.ReservationAt >= startDate && r.ReservationAt <= endDate)
                .OrderBy(r => r.ReservationAt)
                .ToList();
        }
    }
}
