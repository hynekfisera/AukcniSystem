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
			return View((_context.Klienti.Where(x => x.Id == _userManager.GetUserId(User)).Select(x => x.Zustatek).SingleOrDefault(), _context.Aukce.Where(x => x.AutorId == _userManager.GetUserId(User)).ToList(), _context.Prihozy.Where(x => x.KlientId == _userManager.GetUserId(User)).ToList()));
		}

		[HttpPost]
		public IActionResult Prehled([FromForm] int novyZustatek)
		{
			var user = _context.Klienti.Where(x => x.Id == _userManager.GetUserId(User)).SingleOrDefault();
			if (user != null)
			{
				user.Zustatek = novyZustatek;
				_context.SaveChanges();
			}
			return View("Prehled", (_context.Klienti.Where(x => x.Id == _userManager.GetUserId(User)).Select(x => x.Zustatek).SingleOrDefault(), _context.Aukce.Where(x => x.AutorId == _userManager.GetUserId(User)).ToList(), _context.Prihozy.Where(x => x.KlientId == _userManager.GetUserId(User)).ToList()));
		}

		public IActionResult NovaAukce()
		{
			return View((_context.Klienti.Where(x => x.Id == _userManager.GetUserId(User)).Select(x => x.Zustatek).SingleOrDefault(), _context.Kategorie.ToList()));
		}

		[HttpPost]
		public IActionResult NovaAukce([Bind("KategorieId,Nazev,Popis,Cena,Foto,Datum,PrihozeniPoCastce,MinimalniPrihoz,DobaTrvani")] Aukce aukce)
		{
			var user = _context.Klienti.Where(x => x.Id == _userManager.GetUserId(User)).SingleOrDefault();
			if (user != null)
			{
				aukce.AutorId = user.Id;
				if (user.Zustatek >= 10)
				{
					user.Zustatek = user.Zustatek - 10;
					_context.Aukce.Add(aukce);
					_context.SaveChanges();
				}
			}
			return View("Prehled", (_context.Klienti.Where(x => x.Id == _userManager.GetUserId(User)).Select(x => x.Zustatek).SingleOrDefault(), _context.Aukce.Where(x => x.AutorId == _userManager.GetUserId(User)).ToList(), _context.Prihozy.Where(x => x.KlientId == _userManager.GetUserId(User)).ToList()));
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}