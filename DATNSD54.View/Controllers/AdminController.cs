using Microsoft.AspNetCore.Mvc;

namespace DATNSD54.View.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
