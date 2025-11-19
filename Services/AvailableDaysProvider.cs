using ReservationSystem.Models;
using ReservationSystem.Repositories;
using ReservationSystem.Util;
using ReservationSystem.ViewModels;
using System.Runtime.CompilerServices;

namespace ReservationSystem.Services
{
    public class AvailableDaysProvider
    {
        private const int COUNT_DAYS = 7;
        private DateTime currentDateTime = DateTime.Now;
        private readonly IReservationRepository _reservationRepo;
        private readonly SysConstUtil _sysConstUtil;
        public AvailableDaysProvider(IReservationRepository reservationRepo, SysConstUtil sysConstUtil)
        {
            _reservationRepo = reservationRepo;
            _sysConstUtil = sysConstUtil;
        }

        // Hlavná metóda na získanie dostupných dní a časov
        public async Task<AvailableDaysModel> GetAvailableDays(int requiredDuration)
        {
            var model = new AvailableDaysModel
            {
                Days = new List<AvailableDateTimeModel>()
            };

            // Získanie platných dní
            SysConst? sysConst = await _sysConstUtil.GetSysConstAsync();
            DateTime dateNow = currentDateTime.Date;
            int addedDays = 0;
            List<DateTime> validDays = new List<DateTime>();

            while (addedDays < COUNT_DAYS)
            {
                if (await IsValidDay(dateNow))
                {
                    validDays.Add(dateNow);
                    addedDays++;
                }
                dateNow = dateNow.AddDays(1);
            }

            // Načítanie existujúcich rezervácií pre obdobie
            DateTime startDate = validDays.First();
            DateTime endDate = validDays.Last().AddDays(1);
            List<Reservation> reservations = _reservationRepo.GetReservationsInDatePeriod(startDate, endDate);

            foreach (DateTime day in validDays)
            {
                // filtrujeme rezervácie iba pre daný deň
                var dayReservations = reservations.Where(r => r.ReservationAt.Date == day).ToList();

                // dostupné časy
                var availableTimes = GetAvailableTimes(day, requiredDuration, dayReservations, sysConst);

                if (availableTimes.Any())
                {
                    model.Days.Add(new AvailableDateTimeModel
                    {
                        Date = DateTimeUtil.GetDisplayDate(day),
                        Times = availableTimes
                    });
                }
            }

            return model;
        }

        // Vypočíta dostupné časové sloty pre daný deň
        private List<string> GetAvailableTimes(DateTime date, int requiredDuration, List<Reservation> dayReservations, SysConst sysConst)
        {
            var intervals = CreateIntervals(date, sysConst);
            bool[] isFree = MarkReservedSlots(intervals, dayReservations, sysConst);

            var result = new List<string>();
            int requiredSlots = (int)Math.Ceiling(requiredDuration / sysConst.IntervalTime.TotalMinutes); // počet 15-min intervalov potrebných pre službu

            for (int i = 0; i <= intervals.Count - requiredSlots; i++)
            {
                bool canFit = true;
                for (int j = 0; j < requiredSlots; j++)
                {
                    if (!isFree[i + j])
                    {
                        canFit = false;
                        break;
                    }
                }
                if (canFit)
                {
                    result.Add(DateTimeUtil.GetDisplayTime(intervals[i]));
                }
            }

            return result;
        }

        // Vytvorenie všetkých možných intervalov medzi START_TIME a END_TIME
        private List<DateTime> CreateIntervals(DateTime date, SysConst sysConst)
        {
            var intervals = new List<DateTime>();
            for (TimeSpan t = sysConst.OpenTime; t < sysConst.CloseTime; t = t.Add(sysConst.IntervalTime))
            {
                DateTime dt = date.Add(t);
                if (IsValidTime(dt, sysConst))
                    intervals.Add(dt);
            }
            return intervals;
        }

        // Označenie obsadených slotov
        private bool[] MarkReservedSlots(List<DateTime> intervals, List<Reservation> dayReservations, SysConst sysConst)
        {
            bool[] isFree = Enumerable.Repeat(true, intervals.Count).ToArray();

            foreach (Reservation res in dayReservations)
            {
                DateTime start = res.ReservationAt;
                DateTime end = start.AddMinutes(res.ReservationItem.DurationMinutes);

                for (int i = 0; i < intervals.Count; i++)
                {
                    DateTime intervalStart = intervals[i];
                    DateTime intervalEnd = intervalStart.Add(sysConst.IntervalTime);

                    if (intervalStart < end && intervalEnd > start)
                        isFree[i] = false;
                }
            }

            return isFree;
        }

        private async Task<bool> IsValidDay(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }
            if (await _sysConstUtil.IsClosedOnDay(date))
            {
                return false;
            }
            return true;
        }

        private bool IsValidTime(DateTime dateTime, SysConst sysConst)
        {
            return dateTime >= DateTime.Now.Add(sysConst.DelayTime);
        }
    }
}
