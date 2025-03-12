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
                    title = $"Workout {s.User.TrainingPlan.Name} at {s.ScheduledDate:HH:mm}",
                    start = s.ScheduledDate.ToString("yyyy-MM-ddTHH:mm")
                })
                .ToListAsync();

            return Json(sessions);
        }

        //[HttpGet]
        //public async Task<JsonResult> GetUserExercises()
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var user = await _context.Users
        //        .Where(u => u.Id == userId)
        //        .SelectMany(u => u.Exercises.Select(e => new { e.Id, e.Name }))
        //        .ToListAsync();

        //    return Json(user);
        //}

        [HttpPost]
        public async Task<JsonResult> CreateSession(DateTime selectedDate, TimeSpan time)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var sessionDateTime = selectedDate + time;

            // Check if a session already exists for the user on the same day
            bool sessionExists = await _context.TrainingSessions
                .AnyAsync(ts => ts.UserId == userId && ts.ScheduledDate.Date == sessionDateTime.Date);

            if (sessionExists)
            {
                return Json(new { success = false, message = "You already have a workout scheduled for this day." });
            }

            var session = new TrainingSession
            {
                ScheduledDate = sessionDateTime,
                UserId = userId
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
