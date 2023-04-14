using AukcniSystem.Data;
using AukcniSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AukcniSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Klient> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<Klient> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_context.Kategorie.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Prehled()
        {
            return View((_context.Aukce.Where(x => x.AutorId == _userManager.GetUserId(User)).ToList(), _context.Klienti.Where(x => x.Id == _userManager.GetUserId(User)).Select(x => x.Zustatek).SingleOrDefault()));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}