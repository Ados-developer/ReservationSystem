using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.ViewModels
{
    public class ReservationItemViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The name of service is required")]
        [Display(Name = "Name of service")]
        public string ItemName { get; set; } = string.Empty;
        [Required(ErrorMessage = "The price of service is required")]
        [Display(Name = "Price of service (€)")]
        public string ItemPrice { get; set; } = string.Empty;
        [Required(ErrorMessage = "The description of service is required")]
        [Display(Name = "Description of service")]
        public string ItemDescription { get; set; } = string.Empty;
        [Required(ErrorMessage = "The duration of service is required")]
        [Display(Name = "Duration in minutes")]
        public string DurationMinutes { get; set; } = string.Empty;
    }
}
