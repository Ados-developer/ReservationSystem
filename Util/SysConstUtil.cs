using ReservationSystem.Models;
using ReservationSystem.Repositories;

namespace ReservationSystem.Util
{
    public class SysConstUtil
    {
        private readonly ISysConstRepository _repository;
        public SysConstUtil(ISysConstRepository repository)
        {
            _repository = repository;
        }
        public async Task<SysConst> GetSysConstAsync()
        {
            var sysConst = await _repository.Get();
            if (sysConst == null)
                sysConst = new SysConst();
            return sysConst;
        }
        public async Task<bool> IsClosedOnDay(DateTime date)
        {
            SysConst? sysConst = await GetSysConstAsync();
            string? closedDaysString = sysConst.ClosedOnDays;
            if (string.IsNullOrEmpty(closedDaysString))
            {
                return false;
            }
            string[] closedDays = closedDaysString.Split(',');
            foreach (string dayString in closedDays)
            {
                string day = dayString.Trim();
                string[] dateParts = day.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (dateParts.Length == 3 &&
                    int.TryParse(dateParts[0], out int d) &&
                    int.TryParse(dateParts[1], out int m) &&
                    int.TryParse(dateParts[2], out int y))
                {
                    if (date.Day == d && date.Month == m && date.Year == y)
                        return true;

                    continue;
                }
                if (dateParts.Length == 2 &&
                    int.TryParse(dateParts[0], out int dateDay) &&
                    int.TryParse(dateParts[1], out int dateMonth))
                {
                    if (date.Day == dateDay && date.Month == dateMonth)
                        return true;
                }
            }
            return false;
        }
    }
}
