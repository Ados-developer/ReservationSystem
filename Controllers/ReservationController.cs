using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Services;
using ReservationSystem.Util;
using ReservationSystem.ViewModels;

namespace ReservationSystem.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IReservationItemService _reservationItemService;
        public ReservationController(IReservationService reservationService, IReservationItemService reservationItemService)
        {
            _reservationService = reservationService;
            _reservationItemService = reservationItemService;
        }
        public async Task<IActionResult> GetRecords()
        {
            List<ReservationViewModel> reservations = await _reservationService.GetAllAsync();
            return View(reservations);
        }
        public async Task<IActionResult> AddRecord()
        {
            List<ReservationItemViewModel> availableItems = await _reservationItemService.GetAllAsync();
            ReservationViewModel model = new ReservationViewModel
            {
                AvailableItems = availableItems,
                CreatedAt = DateTimeUtil.GetDisplayDateTime(DateTime.Now)
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecord(ReservationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableItems = await _reservationItemService.GetAllAsync();
                return View(model);
            }
            await _reservationService.CreateAsync(model);
            TempData["message"] = "Reservation created successfully.";
            return RedirectToAction(nameof(GetRecords));
        }
        public async Task<IActionResult> EditRecord(Guid id)
        {
            ReservationViewModel? reservation = await _reservationService.GetByIdAsync(id);
            if(reservation == null)
            {
                return NotFound();
            }
            reservation.AvailableItems = await _reservationItemService.GetAllAsync();
            return View(reservation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRecord(ReservationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableItems = await _reservationItemService.GetAllAsync();
                return View(model);
            }

            await _reservationService.UpdateAsync(model);
            TempData["message"] = "Reservation edited successfully.";
            return RedirectToAction(nameof(GetRecords));
        }
        public async Task<IActionResult> DeleteRecord(Guid id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
                return NotFound();

            return View(reservation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _reservationService.DeleteAsync(id);
            TempData["message"] = "Reservation removed successfully.";
            return RedirectToAction(nameof(GetRecords));
        }
    }
}
