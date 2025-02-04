using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemForManagingFitnessTrainings.Entities;
using SystemForManagingFitnessTrainings.Helpers;
using SystemForManagingFitnessTrainings.Services;
using SystemForManagingFitnessTrainings.Services.IServices;

namespace SystemForManagingFitnessTrainings.Controllers
{
    [Route("progress")]
    public class ProgressController : Controller
    {
        private readonly IProgressService _progressService;

        public ProgressController(IProgressService progressService)
        {
            _progressService = progressService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = User.Identity.Name; // Retrieve logged-in user ID
            var progress = await _progressService.GetUserProgressAsync(userId);
            return View(progress);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Progress progress)
        {
            if (ModelState.IsValid)
            {
                await _progressService.AddProgressAsync(progress);
                return RedirectToAction("Index");
            }
            return View(progress);
        }
    }
}
