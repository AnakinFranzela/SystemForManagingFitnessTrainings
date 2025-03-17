using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemForManagingFitnessTrainings.ViewModels;
using SystemForManagingFitnessTrainings.Helpers;
using SystemForManagingFitnessTrainings.Services;
using SystemForManagingFitnessTrainings.Services.IServices;

namespace SystemForManagingFitnessTrainings.Controllers
{
    //[Route("progress")]
    public class ProgressController : Controller
    {
        private readonly IProgressService _progressService;
        private readonly ITrainingPlanService _trainingPlanService;

        public ProgressController(IProgressService progressService, ITrainingPlanService trainingPlanService)
        {
            _progressService = progressService;
            _trainingPlanService = trainingPlanService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var progressRecords = await _progressService.GetUserProgressAsync(userId);

            return View(progressRecords);
        }

        // GET: Progress/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.TrainingPlan = await _trainingPlanService.GetUserTrainingPlanAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View();
        }

        // POST: Progress/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProgressViewModel progress)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _progressService.LogProgressAsync(userId, progress.ExerciseId, progress.Date, progress.Results);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.TrainingPlan = await _trainingPlanService.GetUserTrainingPlanAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(progress);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _progressService.DeleteProgressAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
