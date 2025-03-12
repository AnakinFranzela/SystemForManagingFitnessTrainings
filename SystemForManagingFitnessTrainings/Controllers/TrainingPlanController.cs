using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using SystemForManagingFitnessTrainings.Data;
using SystemForManagingFitnessTrainings.Entities;
using SystemForManagingFitnessTrainings.Services;
using SystemForManagingFitnessTrainings.Services.IServices;
using SystemForManagingFitnessTrainings.ViewModels;

namespace SystemForManagingFitnessTrainings.Controllers
{
    public class TrainingPlanController : Controller
    {
        private readonly ITrainingPlanService _trainingPlanService;
        private readonly IExerciseService _exerciseService;

        public TrainingPlanController(ITrainingPlanService trainingPlanService, IExerciseService exerciseService)
        {
            _trainingPlanService = trainingPlanService;
            _exerciseService = exerciseService;
        }

        // GET: TrainingPlans/Create
        [HttpGet]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var hasPlan = await _trainingPlanService.UserHasTrainingPlanAsync(userId);

            var exercises = await _exerciseService.GetAllExercisesAsync();
            ViewBag.Exercises = exercises;

            var viewModel = new TrainingPlanViewModel
            {
                Exercises = exercises
            };

            if (hasPlan)
            {
                return RedirectToAction("Index", new { message = "You already have a training plan." });
            }

            return View(viewModel);
        }

        // POST: TrainingPlans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainingPlanViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var hasPlan = await _trainingPlanService.UserHasTrainingPlanAsync(userId);

                if (hasPlan)
                {
                    ModelState.AddModelError("", "You already have a training plan.");
                    return View(viewModel);
                }

                // Create the training plan
                await _trainingPlanService.CreateTrainingPlanAsync(userId, viewModel.Name, viewModel.Frequency, viewModel.SelectedExercisesIds);

                return RedirectToAction(nameof(Index));
            }

            // Repopulate the dropdown if there are validation errors
            var exercises = await _exerciseService.GetAllExercisesAsync();
            ViewBag.Exercises = exercises;

            return View(viewModel);
        }

        // GET: TrainingPlans/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainingPlan = await _trainingPlanService.GetUserTrainingPlanAsync(userId);

            if (trainingPlan == null)
            {
                return RedirectToAction("Create"); // Redirect to create a new plan if none exists
            }

            return View(trainingPlan);
        }

        // GET: TrainingPlans/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var plan = await _trainingPlanService.GetTrainingPlanByIdAsync(id);
            if (plan == null) return NotFound();

            var viewModel = new TrainingPlanViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Frequency = plan.Frequency,
                SelectedExercisesIds = plan.User.Exercises.Select(e => e.Id).ToList(),
                Exercises = await _exerciseService.GetAllExercisesAsync()
            };

            return View(viewModel);
        }

        // POST: TrainingPlans/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TrainingPlanViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Exercises = await _exerciseService.GetAllExercisesAsync();
                return View(viewModel);
            }

            await _trainingPlanService.UpdateTrainingPlanAsync(viewModel.Id.Value, viewModel.Name, viewModel.Frequency, viewModel.SelectedExercisesIds);
            return RedirectToAction("Index");
        }

        // GET: TrainingPlans/Delete
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                await _trainingPlanService.DeleteTrainingPlanAsync(id);
                return Json(new { success = true, message = "Успешно изтрит." });
            }
            catch
            {
                return Json(new { success = false, message = "Неуспешно изтриване." }); 
            }
        }

        //// POST: TrainingPlans/DeleteConfirmed
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    await _trainingPlanService.DeleteTrainingPlanAsync(id);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}