using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Repositories;
using ReservationSystem.Services;
using ReservationSystem.ViewModels;

namespace ReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationApiController : ControllerBase
    {
        private readonly AvailableDaysProvider _availableDaysProvider;

        public ReservationApiController(AvailableDaysProvider availableDaysProvider)
        {
            _availableDaysProvider = availableDaysProvider;
        }
        [HttpGet]
        public async Task<ActionResult<AvailableDaysModel>> GetAvailableDateTimes([FromQuery] int duration)
        {
            AvailableDaysModel model = await _availableDaysProvider.GetAvailableDays(duration);
            return Ok(model);
        }
    }
}
