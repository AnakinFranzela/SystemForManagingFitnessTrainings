using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemForManagingFitnessTrainings.Helpers;

namespace SystemForManagingFitnessTrainings.Controllers
{
    [Authorize(Roles = StaticData.Role_Admin)]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
