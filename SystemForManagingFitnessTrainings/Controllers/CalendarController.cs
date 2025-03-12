using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SystemForManagingFitnessTrainings.Data;
using SystemForManagingFitnessTrainings.Entities;
using SystemForManagingFitnessTrainings.ViewModels;

namespace SystemForManagingFitnessTrainings.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalendarController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetSessions()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var sessions = await _context.TrainingSessions
                .Where(s => s.UserId == userId)
                .Select(s => new {
                    id = s.Id,
                    title = $"Workout ({s.Exercises.Count} exercises)",
                    start = s.ScheduledDate.ToString("yyyy-MM-dd")
                })
                .ToListAsync();

            return Json(sessions);
        }

        [HttpGet]
        public async Task<JsonResult> GetUserExercises()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Exercises.Select(e => new { e.Id, e.Name }))
                .ToListAsync();

            return Json(user);
        }

        [HttpPost]
        public async Task<JsonResult> CreateSession([FromBody] CalendarViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exercises = await _context.Exercises
                .Where(e => model.ExerciseIds.Contains(e.Id))
                .ToListAsync();

            var session = new TrainingSession
            {
                ScheduledDate = model.Date,
                UserId = userId,
                Exercises = exercises
            };

            _context.TrainingSessions.Add(session);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    

        // Delete a session
        [HttpPost]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var session = await _context.TrainingSessions.FindAsync(id);
            if (session != null)
            {
                _context.TrainingSessions.Remove(session);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }
    }
}
