using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using SystemForManagingFitnessTrainings.Entities;
using SystemForManagingFitnessTrainings.Services;
using SystemForManagingFitnessTrainings.Services.IServices;

namespace SystemForManagingFitnessTrainings.Controllers
{
    public class TrainingPlanController : Controller
    {
        private readonly TrainingPlanService _trainingPlanService;

        public TrainingPlanController(TrainingPlanService trainingPlanService)
        {
            _trainingPlanService = trainingPlanService;
        }

        // GET: TrainingPlans/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var hasPlan = await _trainingPlanService.UserHasTrainingPlanAsync(userId);

            if (hasPlan)
            {
                return RedirectToAction("Index", new { message = "You already have a training plan." });
            }

            return View();
        }

        // POST: TrainingPlans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Frequency")] TrainingPlan trainingPlan)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var hasPlan = await _trainingPlanService.UserHasTrainingPlanAsync(userId);

                if (hasPlan)
                {
                    ModelState.AddModelError("", "You already have a training plan.");
                    return View(trainingPlan);
                }

                await _trainingPlanService.CreateTrainingPlanAsync(userId, trainingPlan.Name, trainingPlan.Frequency);

                return RedirectToAction(nameof(Index));
            }

            return View(trainingPlan);
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
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainingPlan = await _trainingPlanService.GetUserTrainingPlanAsync(userId);

            if (trainingPlan == null || trainingPlan.Id != id)
            {
                return NotFound();
            }

            return View(trainingPlan);
        }

        // POST: TrainingPlans/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Frequency")] TrainingPlan trainingPlan)
        {
            if (id != trainingPlan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var success = await _trainingPlanService.UpdateTrainingPlanAsync(id, trainingPlan.Name, trainingPlan.Frequency);

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(trainingPlan);
        }
    }
}