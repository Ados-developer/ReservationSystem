using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationSystem.Models
{
    public class SysConst
    {
        public Guid Id { get; set; }
        [Column(TypeName = "time")]
        public TimeSpan OpenTime { get; set; } = new TimeSpan(9, 0, 0);
        [Column(TypeName = "time")]
        public TimeSpan CloseTime { get; set; } = new TimeSpan(17,0,0);
        [Column(TypeName = "time")]
        public TimeSpan IntervalTime { get; set; } = new TimeSpan(0, 15,0);
        [Column(TypeName = "time")]
        public TimeSpan DelayTime { get; set; } = new TimeSpan(1, 0, 0);
        public string? ClosedOnDays { get; set; }
    }
}
