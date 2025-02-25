using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemForManagingFitnessTrainings.Data;
using SystemForManagingFitnessTrainings.Entities;
using SystemForManagingFitnessTrainings.Services;

namespace SystemForManagingFitnessTrainings.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly ExerciseService _exerciseService;

        public ExerciseController(ExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        // GET: Exercises/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var exercises = await _exerciseService.GetAllExercisesAsync();
            return View(exercises);
        }

        // GET: Exercises/Create
        [HttpGet]
        [Authorize(Roles = "Admin")] // Only admins can create exercises
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Only admins can create exercises
        public async Task<IActionResult> Create([Bind("Name,Category,Description")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                await _exerciseService.CreateExerciseAsync(exercise.Name, exercise.Category, exercise.Description);
                return RedirectToAction(nameof(Index));
            }

            return View(exercise);
        }

        // GET: Exercises/Edit/5
        [HttpGet]
        [Authorize(Roles = "Admin")] // Only admins can edit exercises
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _exerciseService.GetExercisesByCategoryAsync("All");/*FirstOrDefaultAsync(e => e.Id == id.Value);*/

            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Only admins can edit exercises
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Category,Description")] Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var success = await _exerciseService.UpdateExerciseAsync(id, exercise.Name, exercise.Category, exercise.Description);

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(exercise);
        }

        // GET: Exercises/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin")] // Only admins can delete exercises
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _exerciseService.GetExercisesByCategoryAsync("All");/*FirstOrDefaultAsync(e => e.Id == id.Value);*/

            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Only admins can delete exercises
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _exerciseService.DeleteExerciseAsync(id);

            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
