using Alee_BulkyWeb.Data;
using Alee_BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alee_BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View();
        }
    }
}