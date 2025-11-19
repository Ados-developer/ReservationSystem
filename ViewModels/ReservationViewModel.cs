using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.ViewModels
{
    public class ReservationViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Customer name is required")]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Customer E-mail is required")]
        [EmailAddress(ErrorMessage = "Invalid E-mail")]
        [Display(Name = "Customer E-mail")]
        public string CustomerEmail { get; set; } = string.Empty;
        [Required(ErrorMessage = "Customer phone is required")]
        [Display(Name = "Customer Phone")]
        public string CustomerPhone { get; set; } = string.Empty;
        [Display(Name = "Note")]
        public string? Note { get; set; }
        [Required]
        [Display(Name = "Created At")]
        public string CreatedAt { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Reservation At")]
        public string ReservationAt { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Vyberte službu")]
        public Guid ReservationItemId { get; set; }
        public string? ReservationItemName { get; set; }
        public string? ReservationItemPrice { get; set; }
        public string? ReservationItemDuration { get; set; }
        public List<ReservationItemViewModel>? AvailableItems { get; set; }
    }
}
