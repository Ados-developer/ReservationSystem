using ReservationSystem.Models;
using ReservationSystem.Util;
using ReservationSystem.ViewModels;

namespace ReservationSystem.Mappers
{
    public class SysConstMapper
    {
        public static SysConstViewModel ToViewModel(SysConst src)
        {
            return new SysConstViewModel
            {
                Id = src.Id,
                OpenTime = DateTimeUtil.GetDisplayTime(DateTime.Today.Add(src.OpenTime)),
                CloseTime = DateTimeUtil.GetDisplayTime(DateTime.Today.Add(src.CloseTime)),
                IntervalTime = DateTimeUtil.GetDisplayTime(DateTime.Today.Add(src.IntervalTime)),
                DelayTime = DateTimeUtil.GetDisplayTime(DateTime.Today.Add(src.DelayTime)),
                ClosedOnDays = src.ClosedOnDays
            };
        }
        public static SysConst ToModel(SysConstViewModel src)
        {
            return new SysConst
            {
                Id = src.Id,
                OpenTime = DateTimeUtil.DisplayTimeToTime(src.OpenTime) ?? new TimeSpan(9, 0, 0),
                CloseTime = DateTimeUtil.DisplayTimeToTime(src.CloseTime) ?? new TimeSpan(17, 0, 0),
                IntervalTime = DateTimeUtil.DisplayTimeToTime(src.IntervalTime) ?? new TimeSpan(0, 15, 0),
                DelayTime = DateTimeUtil.DisplayTimeToTime(src.DelayTime) ?? new TimeSpan(1, 0, 0),
                ClosedOnDays = src.ClosedOnDays
            };
        }
    }
}
