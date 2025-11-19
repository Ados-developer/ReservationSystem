using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.ViewModels
{
    public class SysConstViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Open from is required")]
        [Display(Name = "Open from")]
        public string OpenTime { get; set; } = String.Empty;
        [Required(ErrorMessage = "Open to is required")]
        [Display(Name = "Open to")]
        public string CloseTime { get; set; } = String.Empty;
        [Required(ErrorMessage = "Interval time is required")]
        [Display(Name = "Interval time")]
        public string IntervalTime {  get; set; } = String.Empty;
        [Required(ErrorMessage = "Delay time is required")]
        [Display(Name = "Delay time")]
        public string DelayTime { get; set; } = String.Empty;
        [Display(Name = "Closed on Days")]
        public string? ClosedOnDays {  get; set; }
    }
}
