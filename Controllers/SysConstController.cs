using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Services;
using ReservationSystem.ViewModels;

namespace ReservationSystem.Controllers
{
    public class SysConstController : Controller
    {
        private readonly ISysConstService _service;
        public SysConstController(ISysConstService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Edit()
        {
            SysConstViewModel viewModel = await _service.GetSysConstAsync();
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SysConstViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            await _service.UpdateSysConst(viewModel);
            TempData["Message"] = "System options saved successfully.";
            return RedirectToAction(nameof(Edit));
        }
    }
}
