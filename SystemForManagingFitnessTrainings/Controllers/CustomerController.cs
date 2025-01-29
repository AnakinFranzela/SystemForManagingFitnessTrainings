using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemForManagingFitnessTrainings.Helpers;

namespace SystemForManagingFitnessTrainings.Controllers
{
    public class CustomerController : Controller
    {
        [Authorize(Roles = StaticData.Role_Customer)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
