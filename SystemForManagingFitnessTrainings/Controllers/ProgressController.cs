using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemForManagingFitnessTrainings.ViewModels;
using SystemForManagingFitnessTrainings.Helpers;
using SystemForManagingFitnessTrainings.Services;
using SystemForManagingFitnessTrainings.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace SystemForManagingFitnessTrainings.Controllers
{
    [Authorize]
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
            bool userHasTrainingPlan = await _trainingPlanService.UserHasTrainingPlanAsync(userId);

            if (userHasTrainingPlan == false)
            {
                // Option A: Redirect to the TrainingPlan creation page
                return RedirectToAction("Create", "TrainingPlan", new { message = "Моля първо създайте тренировъчен план." });
            }

            var progressRecords = await _progressService.GetUserProgressAsync(userId);

            ViewBag.TrainingPlan = await _trainingPlanService.GetUserTrainingPlanAsync(userId);

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
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var resultString = ProgressResultHelper.ToResultString(progress.Weight, progress.Repetition, progress.TimeInSeconds);

                await _progressService.LogProgressAsync(userId, progress.ExerciseId, progress.Date, resultString);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.TrainingPlan = await _trainingPlanService.GetUserTrainingPlanAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(progress);
        }

        [HttpGet]
        public async Task<JsonResult> GetExerciseProgressData(int exerciseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Get all progress only for the chosen exercise
            var progressList = await _progressService.GetExerciseProgressAsync(userId, exerciseId);

            // Convert each record’s Results string into numeric values
            var data = progressList.Select(p =>
            {
                var (weight, reps, timeSec) = ProgressResultHelper.FromResultString(p.Results);
                return new
                {
                    date = p.Date.ToString("yyyy-MM-dd"),
                    weight,
                    reps,
                    timeSec
                };
            });

            return Json(data);
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
