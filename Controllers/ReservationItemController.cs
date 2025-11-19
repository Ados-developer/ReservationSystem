using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Models;
using ReservationSystem.Services;
using ReservationSystem.ViewModels;

namespace ReservationSystem.Controllers
{
    public class ReservationItemController : Controller
    {
        private readonly IReservationItemService _service;
        public ReservationItemController(IReservationItemService service)
        {
            _service = service;
        }
        public async Task<IActionResult> GetRecords()
        {
            List<ReservationItemViewModel> items = await _service.GetAllAsync();
            return View(items);
        }
        public IActionResult AddRecord()
        {
            return View(new ReservationItemViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecord(ReservationItemViewModel item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }
            await _service.CreateAsync(item);
            TempData["message"] = "Item was created successfully.";
            return RedirectToAction(nameof(GetRecords));
        }
        public async Task<IActionResult> EditRecord(Guid id)
        {
            ReservationItemViewModel? item = await _service.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRecord(ReservationItemViewModel item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }
            await _service.UpdateAsync(item);
            TempData["message"] = "Item was edited successfully.";
            return RedirectToAction(nameof(GetRecords));
        }
        public async Task<IActionResult> DeleteRecord(Guid id)
        {
            ReservationItemViewModel? item = await _service.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeleteAsync(id);
            TempData["message"] = "Item was removed succesfully.";
            return RedirectToAction(nameof(GetRecords));
        }
    }
}
