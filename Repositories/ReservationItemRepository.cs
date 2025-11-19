using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Models;

namespace ReservationSystem.Repositories
{
    public class ReservationItemRepository : IReservationItemRepository
    {
        private readonly ReservationSystemDbContext _context;
        public ReservationItemRepository(ReservationSystemDbContext context)
        {
            _context = context;
        }
        public async Task<List<ReservationItem>> GetAllAsync()
        {
            return await _context.ReservationItems.ToListAsync();
        }
        public async Task<ReservationItem?> GetByIdAsync(Guid id)
        {
            return await _context.ReservationItems.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task AddAsync(ReservationItem reservationItem)
        {
            await _context.ReservationItems.AddAsync(reservationItem);
        }
        public void Update(ReservationItem reservationItem)
        {
             _context.ReservationItems.Update(reservationItem);
        }
        public async Task DeleteAsync(Guid id)
        {
            ReservationItem? reservationItem = await _context.ReservationItems.FirstOrDefaultAsync(i => i.Id == id);
            if (reservationItem != null)
            {
                _context.Remove(reservationItem);
            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
