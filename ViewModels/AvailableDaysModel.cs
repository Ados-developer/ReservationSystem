namespace ReservationSystem.ViewModels
{
    public class AvailableDaysModel
    {
        public List<AvailableDateTimeModel>? Days { get; set; }
    }
    public class AvailableDateTimeModel
    {
        public string? Date { get; set; }
        public List<string>? Times { get; set; }
    }
}
