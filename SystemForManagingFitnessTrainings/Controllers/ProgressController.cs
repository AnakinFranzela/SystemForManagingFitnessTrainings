using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemForManagingFitnessTrainings.Entities;
using SystemForManagingFitnessTrainings.Helpers;
using SystemForManagingFitnessTrainings.Services;
using SystemForManagingFitnessTrainings.Services.IServices;

namespace SystemForManagingFitnessTrainings.Controllers
{
    //[Route("progress")]
    public class ProgressController : Controller
    {
        private readonly IProgressService _progressService;

        public ProgressController(IProgressService progressService)
        {
            _progressService = progressService;
        }

        // GET: Progress/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Progress/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Results,ExerciseId")] Progress progress)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _progressService.LogProgressAsync(userId, progress.ExerciseId, progress.Date, progress.Results);

                return RedirectToAction(nameof(Index));
            }

            return View(progress);
        }

        // GET: Progress/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var progresses = await _progressService.GetUserProgressAsync(userId);

            return View(progresses);
        }
    }
}
