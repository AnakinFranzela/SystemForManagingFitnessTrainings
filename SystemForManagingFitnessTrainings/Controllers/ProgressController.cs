using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemForManagingFitnessTrainings.Entities;
using SystemForManagingFitnessTrainings.Helpers;
using SystemForManagingFitnessTrainings.Repositories.IRepositories;

namespace SystemForManagingFitnessTrainings.Controllers
{
    [Route("progress")]
    public class ProgressController : Controller
    {
        private readonly IProgressRepository _progressRepository;

        public ProgressController(IProgressRepository progressRepository)
        {
            _progressRepository = progressRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = User.Identity.Name; // Retrieve logged-in user ID
            var progress = await _progressRepository.GetUserProgressAsync(userId);
            return View(progress);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Progress progress)
        {
            if (ModelState.IsValid)
            {
                await _progressRepository.AddProgressAsync(progress);
                return RedirectToAction("Index");
            }
            return View(progress);
        }
    }
}
