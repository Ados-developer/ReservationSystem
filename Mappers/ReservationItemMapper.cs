using ReservationSystem.Models;
using ReservationSystem.Util;
using ReservationSystem.ViewModels;

namespace ReservationSystem.Mappers
{
    public static class ReservationItemMapper
    { 
        public static ReservationItemViewModel ToViewModel(ReservationItem src)
        {
            return new ReservationItemViewModel
            {
                Id = src.Id,
                ItemName = src.ItemName,
                ItemPrice = PriceUtil.NumberToEditorString(src.ItemPrice),
                ItemDescription = src.ItemDescription,
                DurationMinutes = PriceUtil.IntNumberToEditorString(src.DurationMinutes)
            };
        }
        public static ReservationItem ToModel(ReservationItemViewModel src)
        {
            return new ReservationItem
            {
                Id = src.Id,
                ItemName = src.ItemName,
                ItemPrice = PriceUtil.NumberFromEditorString(src.ItemPrice),
                ItemDescription = src.ItemDescription,
                DurationMinutes = PriceUtil.IntNumberFromEditorString(src.DurationMinutes)
            };
        }
        
    }
}
