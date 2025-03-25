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
using SystemForManagingFitnessTrainings.Helpers;
using NuGet.Protocol.Plugins;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

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
        public async Task<IActionResult> Index(string category, int page = 1, int pageSize = 5)
        {
            var exercises = string.IsNullOrEmpty(category) || category == "All"
                ? await _exerciseService.GetAllExercisesAsync()
                : await _exerciseService.GetExercisesByCategoryAsync(category);


            int totalCount = exercises.Count();
            var exercisesPage = exercises
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new ExerciseTableViewModel
            {
                Exercises = exercisesPage,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                SelectedCategory = category ?? "All",
                // You may keep your categories list in here as well
            };

            var categoriesList = Enum.GetValues(typeof(Categories))
                .Cast<Categories>()
                .Select(c => new SelectListItem
                {
                    Text = c.ToString(),
                    Value = c.ToString(),
                    Selected = (c.ToString() == category)
                }).ToList();

            ViewBag.Categories = categoriesList;

            ViewBag.SelectedCategory = string.IsNullOrEmpty(category) ? "All" : category;

            return View(viewModel);
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
                if (await _exerciseService.CheckForExistingExercise(viewModel.Name))
                {
                    await _exerciseService.CreateExerciseAsync(viewModel.Name, viewModel.Category.ToString(), viewModel.Description);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Categories = new SelectList(Enum.GetValues(typeof(Categories)), viewModel.Category);
                //TempData["CustomError"] = "Тази тренировка вече съществува.";
                ModelState.AddModelError("CustomError", "Тази тренировка вече съществува.");
                return View(viewModel);
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
