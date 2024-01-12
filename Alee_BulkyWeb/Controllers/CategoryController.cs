using Microsoft.AspNetCore.Mvc;

namespace Alee_BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
