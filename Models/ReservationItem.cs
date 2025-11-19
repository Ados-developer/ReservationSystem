using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models
{
    public class ReservationItem
    {
        public Guid Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal ItemPrice { get; set; }
        public string ItemDescription { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
    }
}
