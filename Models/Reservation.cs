
namespace ReservationSystem.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ReservationAt { get; set; }
        public Guid ReservationItemId { get; set; }
        public ReservationItem ReservationItem { get; set; } = null!;
    }
}
