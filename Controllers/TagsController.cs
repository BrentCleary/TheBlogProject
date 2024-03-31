using Microsoft.AspNetCore.Mvc;
using TheBlogProject.Data;

namespace TheBlogProject.Controllers
{
    public class TagsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public TagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
