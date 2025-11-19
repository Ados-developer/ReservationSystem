using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Models;

namespace ReservationSystem.Repositories
{
    public class SysConstRepository : ISysConstRepository
    {
        private readonly ReservationSystemDbContext _context;
        public SysConstRepository(ReservationSystemDbContext context)
        {
            _context = context;
        }
        public async Task<SysConst> Get()
        {
            SysConst? sysConst = await _context.SystemConsts.FirstOrDefaultAsync();
            if (sysConst == null)
            {
                sysConst = new SysConst();
                _context.SystemConsts.Add(sysConst);
                await _context.SaveChangesAsync();
            }
            return sysConst;
        }
        public async Task Update(SysConst sysConst)
        {
            SysConst? existingSysConst = await _context.SystemConsts.FirstOrDefaultAsync();
            if (existingSysConst == null)
            {
                _context.SystemConsts.Add(sysConst);
            }
            else
            {
                _context.Entry(existingSysConst).CurrentValues.SetValues(sysConst);
            }
            await _context.SaveChangesAsync();
        }
    }
}
