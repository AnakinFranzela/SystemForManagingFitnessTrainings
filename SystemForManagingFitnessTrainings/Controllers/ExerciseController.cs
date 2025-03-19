using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SystemForManagingFitnessTrainings.Data;
using SystemForManagingFitnessTrainings.ViewModels;
using SystemForManagingFitnessTrainings.Enums;
using SystemForManagingFitnessTrainings.Services;
using SystemForManagingFitnessTrainings.Services.IServices;
using Microsoft.IdentityModel.Tokens;

namespace SystemForManagingFitnessTrainings.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        // GET: Exercises/Index
        [HttpGet]
        public async Task<IActionResult> Index(string category)
        {
            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(Categories)));

            var exercises = category.IsNullOrEmpty() || category == "All" ? await _exerciseService.GetAllExercisesAsync() : await _exerciseService.GetExercisesByCategoryAsync(category);

            ViewBag.SelectedCategory = category ?? "All";
            return View(exercises);
        }

        // GET: Exercises/Create
        [HttpGet]
        [Authorize(Roles = "Admin")] // Only admins can create exercises
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(Categories)));
            return View();
        }

        // POST: Exercises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Only admins can create exercises
        public async Task<IActionResult> Create(ExerciseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _exerciseService.CreateExerciseAsync(viewModel.Name, viewModel.Category.ToString(), viewModel.Description);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(Categories)), viewModel.Category);
            return View(viewModel);
        }

        // GET: Exercises/Edit/5
        [HttpGet]
        [Authorize(Roles = "Admin")] // Only admins can edit exercises
        public async Task<IActionResult> Edit(int id)
        {
            var exercise = await _exerciseService.GetExerciseByIdAsync(id);

            var viewModel = new ExerciseViewModel
            {
                Name = exercise.Name,
                Category = Enum.Parse<Categories>(exercise.Category),
                Description = exercise.Description
            };

            if (exercise == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(Categories)), viewModel.Category);

            return View(viewModel);
        }

        // POST: Exercises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Only admins can edit exercises
        public async Task<IActionResult> Edit(int id, ExerciseViewModel viewModel)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                var success = await _exerciseService.UpdateExerciseAsync(id, viewModel.Name, viewModel.Category.ToString(), viewModel.Description);

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(Categories)), viewModel.Category);
            return View(viewModel);
        }

        // GET: Exercises/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")] // Only admins can delete exercises
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                await _exerciseService.DeleteExerciseAsync(id);
                return Json(new { success = true, message = "Успешно изтрит." });
            }
            catch
            {
                return Json(new { success = false, message = "Неуспешно изтриване." });
            }
        }
    }
}
