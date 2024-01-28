using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using TheBlogProject.Data;
using TheBlogProject.Models;
using TheBlogProject.Services;
using TheBlogProject.ViewModels;
using X.PagedList;

namespace TheBlogProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _imageService;

        public HomeController(ILogger<HomeController> logger, IBlogEmailSender emailSender, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IImageService imageService)
        {
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 5;

            //var blogs = _context.Blogs.Where(
            //    b => b.Posts.Any(p => p.ReadyStatus == Enums.ReadyStatus.ProductionReady))
            //    .OrderByDescending(b => b.Created)
            //    .ToPagedListAsync(pageNumber, pageSize);

            var blogs = _context.Blogs
                .Include(b => b.BlogUser)
                .OrderByDescending(b => b.Created)
                .ToPagedListAsync(pageNumber, pageSize);


            ViewData["HeaderImage"] = "/images/blue_grid_background.jpg";
            ViewData["MainText"] = ".NET Developing";
            ViewData["SubText"] = "A Blog About Building in .NET";

            return View(await blogs);
        
        }

        public IActionResult About()
        {
            ViewData["HeaderImage"] = "/images/Large_Howls_Background.jpg";
            ViewData["MainText"] = ".NET Developing";
            ViewData["SubText"] = "A Blog About Building in .NET";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["HeaderImage"] = "/images/Mountain.jpg";
            ViewData["MainText"] = ".NET Developing";
            ViewData["SubText"] = "A Blog About Building in .NET";

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactMe model)
        {
            // This is where we will be emailing...
            model.Message = $"{model.Message} <hr/> Phone: {model.Phone}";
            await _emailSender.SendContactEmailAsync(model.Email, model.Name, model.Subject, model.Message);
            
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
