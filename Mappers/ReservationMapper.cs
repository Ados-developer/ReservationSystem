using ReservationSystem.Models;
using ReservationSystem.Util;
using ReservationSystem.ViewModels;

namespace ReservationSystem.Mappers
{
    public static class ReservationMapper
    {
        public static ReservationViewModel ToViewModel(Reservation src)
        {
            return new ReservationViewModel
            {
                Id = src.Id,
                CustomerName = src.CustomerName,
                CustomerEmail = src.CustomerEmail,
                CustomerPhone = src.CustomerPhone,
                Note = src.Note,
                CreatedAt = DateTimeUtil.GetDisplayDateTime(src.CreatedAt, withSeconds: false),
                ReservationAt = DateTimeUtil.GetDisplayDateTime(src.ReservationAt, withSeconds: false),
                ReservationItemId = src.ReservationItemId,
                ReservationItemName = src.ReservationItem.ItemName
            };
        }
        public static Reservation ToModel(ReservationViewModel src)
        {
            return new Reservation
            {
                Id = src.Id == Guid.Empty ? Guid.NewGuid() : src.Id,
                CustomerName = src.CustomerName,
                CustomerEmail = src.CustomerEmail,
                CustomerPhone = src.CustomerPhone,
                Note = src.Note,
                CreatedAt = DateTimeUtil.DisplayDataToDateTime(src.CreatedAt, DateTime.Now, withSeconds: false) ?? DateTime.Now,
                ReservationAt = DateTimeUtil.DisplayDataToDateTime(src.ReservationAt, null, withSeconds: false) ?? DateTime.Now,
                ReservationItemId = src.ReservationItemId,
            };
        }
    }
}
