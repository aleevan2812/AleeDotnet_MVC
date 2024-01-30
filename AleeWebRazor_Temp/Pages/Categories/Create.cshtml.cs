using AleeWebRazor_Temp.Data;
using AleeWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AleeWebRazor_Temp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }
    }
}