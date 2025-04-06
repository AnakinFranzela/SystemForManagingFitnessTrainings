using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SystemForManagingFitnessTrainings.Services.IServices;

namespace SystemForManagingFitnessTrainings.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly ICalendarService _calendarService;
        private readonly ITrainingPlanService _trainingPlanService;

        public CalendarController(ICalendarService calendarService, ITrainingPlanService trainingPlanService)
        {
            _calendarService = calendarService;
            _trainingPlanService = trainingPlanService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool userHasTrainingPlan = _trainingPlanService.UserHasTrainingPlanAsync(userId).Result;
            if (!userHasTrainingPlan)
            {
                // Redirect to the TrainingPlan creation page
                return RedirectToAction("Create", "TrainingPlan", new { message = "Моля първо създайте тренировъчен план." });
            }
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetSessions()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sessions = await _calendarService.GetSessionsAsync(userId);
            return Json(sessions);
        }

        [HttpPost]
        public async Task<JsonResult> CreateSession(DateTime selectedDate, TimeSpan time)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var sessionDateTime = selectedDate + time;

            if (sessionDateTime < DateTime.Now)
                return Json(new { success = false, message = "Не може да насрочвате тренировка за отминали дати." });

            // Check if a session already exists for the user on the same day
            bool sessionExists = await _calendarService.SessionExistsAsync(userId, sessionDateTime);

            if (sessionExists)
            {
                return Json(new { success = false, message = "Вече имате насрочена тренировка за този ден." });
            }

            await _calendarService.CreateSessionAsync(userId, sessionDateTime);

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<JsonResult> EditSession(int selectedSessionId, DateTime selectedDate, TimeSpan time)
        {
            var newDateTime = selectedDate + time;

            if (newDateTime < DateTime.Now)
                return Json(new { success = false, message = "Не може да редактирате тренировка към миналото." });

            try
            {
                await _calendarService.UpdateSessionAsync(selectedSessionId, newDateTime);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false, message = "Не е намерена насрочена тренировка" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSession(int id)
        {
            try
            {
                await _calendarService.DeleteSessionAsync(id);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false, message = "Не е намерена насрочена тренировка" });
            }
        }
    }
}
