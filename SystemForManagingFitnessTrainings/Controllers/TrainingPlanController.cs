using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using SystemForManagingFitnessTrainings.Entities;
using SystemForManagingFitnessTrainings.Services.IServices;

namespace SystemForManagingFitnessTrainings.Controllers
{
    public class TrainingPlanController : Controller
    {
        private ITrainingPlanService _trainingPlanService;
		private static Dictionary<DateTime, string> trainingSessions = new Dictionary<DateTime, string>();

		public TrainingPlanController(ITrainingPlanService trainingPlanService)
        {
            _trainingPlanService = trainingPlanService;
        }

        public IActionResult Index(TrainingPlan viewModel)
        {

            return View(viewModel);
        }

        public IActionResult Create()
        {
            TrainingPlan viewModel = new TrainingPlan();
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TrainingPlan viewModel)
        {

            return View(viewModel);
        }

		// Action to get the session details for a specific date
		public JsonResult GetSessionDetails(DateTime date)
		{
			if (trainingSessions.ContainsKey(date))
			{
				return Json(new { success = true, sessionDetails = trainingSessions[date] });
			}
			else
			{
				return Json(new { success = false });
			}
		}

		// Action to save a session for a specific date (POST method)
		[HttpPost]
		public JsonResult ScheduleTraining(DateTime date, string sessionDetails)
		{
			if (trainingSessions.ContainsKey(date))
			{
				return Json(new { success = false, message = "Session already scheduled for this date." });
			}
			else
			{
				trainingSessions[date] = sessionDetails;  // Save the session
				return Json(new { success = true });
			}
		}
	}
}