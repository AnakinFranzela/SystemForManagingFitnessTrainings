using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using SystemForManagingFitnessTrainings.Entities;
using SystemForManagingFitnessTrainings.Services.IServices;

namespace SystemForManagingFitnessTrainings.Controllers
{
    public class TrainingPlanController : Controller
    {
        private readonly ITrainingPlanService _trainingPlanService;
		private static Dictionary<DateOnly, string> trainingSessions = new Dictionary<DateOnly, string>();

		public TrainingPlanController(ITrainingPlanService trainingPlanService)
        {
            _trainingPlanService = trainingPlanService;
        }

		public async Task<IActionResult> Index()
		{
			var plans = await _trainingPlanService.GetAllAsync();
			return View(plans);
		}

		[HttpGet("create")]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost("create")]
		public async Task<IActionResult> Create(TrainingPlan plan)
		{
			if (ModelState.IsValid)
			{
				await _trainingPlanService.AddAsync(plan);
				return RedirectToAction("Index");
			}
			return View(plan);
		}

		[HttpGet("edit/{id}")]
		public async Task<IActionResult> Edit(int id)
		{
			var plan = await _trainingPlanService.GetByIdAsync(id);
			if (plan == null) return NotFound();
			return View(plan);
		}

		[HttpPost("edit")]
		public async Task<IActionResult> Edit(TrainingPlan plan)
		{
			if (ModelState.IsValid)
			{
				await _trainingPlanService.UpdateAsync(plan);
				return RedirectToAction("Index");
			}
			return View(plan);
		}

		[HttpPost("delete/{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _trainingPlanService.DeleteAsync(id);
			return RedirectToAction("Index");
		}

		//// Action to get the session details for a specific date
		//public JsonResult GetSessionDetails(DateOnly date)
		//{
		//	if (trainingSessions.ContainsKey(date))
		//	{
		//		return Json(new { success = true, sessionDetails = trainingSessions[date] });
		//	}
		//	else
		//	{
		//		return Json(new { success = false });
		//	}
		//}

		//// Action to save a session for a specific date (POST method)
		//[HttpPost]
		//public JsonResult ScheduleTraining(DateOnly date, string sessionDetails)
		//{
		//	if (trainingSessions.ContainsKey(date))
		//	{
		//		return Json(new { success = false, message = "Session already scheduled for this date." });
		//	}
		//	else
		//	{
		//		trainingSessions[date] = sessionDetails;  // Save the session
		//		return Json(new { success = true });
		//	}
		//}
	}
}