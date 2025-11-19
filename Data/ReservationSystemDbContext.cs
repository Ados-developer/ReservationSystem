using Microsoft.EntityFrameworkCore;
using ReservationSystem.Models;

namespace ReservationSystem.Data
{
    public class ReservationSystemDbContext : DbContext
    {
        public ReservationSystemDbContext(DbContextOptions<ReservationSystemDbContext> options) : base(options)
        {
        }
        public DbSet<ReservationItem> ReservationItems { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<SysConst> SystemConsts { get; set; }
    }
}
