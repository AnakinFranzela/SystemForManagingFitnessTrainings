using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using SystemForManagingFitnessTrainings.Entities;
using SystemForManagingFitnessTrainings.Services.IServices;

namespace SystemForManagingFitnessTrainings.Controllers
{
    public class TrainingPlanController : Controller
    {
        private ITrainingPlanService _trainingPlanService;

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
    }
}
