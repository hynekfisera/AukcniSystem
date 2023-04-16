using AukcniSystem.Data;
using AukcniSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AukcniSystem.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;
		private readonly UserManager<Klient> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<Klient> userManager, RoleManager<IdentityRole> roleManager)
		{
			_logger = logger;
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public IActionResult Index()
		{
			/*bool roleExists = await _roleManager.RoleExistsAsync("Admin");
			if (!roleExists)
			{
				await _roleManager.CreateAsync(new IdentityRole("Admin"));
			}
			var user = await _userManager.GetUserAsync(User);
			await _userManager.AddToRoleAsync(user, "Admin");
			bool isInRole = User.IsInRole("Admin");*/
			return View((_context.Kategorie.ToList(), _context.Aukce.Where(x => x.Datum.Value.AddHours(x.DobaTrvani).Date == DateTime.Today.Date).Take(10).ToList()));
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Prehled()
		{
			return View((_context.Klienti.Where(x => x.Id == _userManager.GetUserId(User)).Select(x => x.Zustatek).SingleOrDefault(), _context.Aukce.Where(x => x.AutorId == _userManager.GetUserId(User)).ToList(), _context.Prihozy.Where(x => x.KlientId == _userManager.GetUserId(User)).Include(x => x.Aukce).ToList()));
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
			return View("Prehled", (_context.Klienti.Where(x => x.Id == _userManager.GetUserId(User)).Select(x => x.Zustatek).SingleOrDefault(), _context.Aukce.Where(x => x.AutorId == _userManager.GetUserId(User)).ToList(), _context.Prihozy.Where(x => x.KlientId == _userManager.GetUserId(User)).Include(x => x.Aukce).ToList()));
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
				aukce.Datum = DateTime.Now;
				if (user.Zustatek >= 10)
				{
					user.Zustatek = user.Zustatek - 10;
					_context.Aukce.Add(aukce);
					_context.SaveChanges();
				}
			}
			return View("Prehled", (_context.Klienti.Where(x => x.Id == _userManager.GetUserId(User)).Select(x => x.Zustatek).SingleOrDefault(), _context.Aukce.Where(x => x.AutorId == _userManager.GetUserId(User)).ToList(), _context.Prihozy.Where(x => x.KlientId == _userManager.GetUserId(User)).ToList()));
		}

		[Authorize]
		[Authorize(Roles = "Admin,Ucetni,Supervizor")]
		public IActionResult AukceNaSchvaleni()
		{
			return View(_context.Aukce.Where(x => x.Schvalena == false).ToList());
		}

		[HttpPost]
		[Authorize]
		[Authorize(Roles = "Admin,Ucetni,Supervizor")]
		public IActionResult AukceNaSchvaleni([FromForm] int AukceId)
		{
			var aukce = _context.Aukce.Where(x => x.AukceId == AukceId).FirstOrDefault();
			if (aukce != null)
			{
				aukce.Schvalena = true;
				aukce.Datum = DateTime.Now;
				_context.SaveChanges();
			}
			return View(_context.Aukce.Where(x => x.Schvalena == false).ToList());
		}

		public IActionResult Aukce([FromRoute] string id)
		{
			int AukceId = Int32.Parse(id);
			var aukce = _context.Aukce.Where(x => x.AukceId == AukceId).Include(x => x.Autor).FirstOrDefault();
			if (aukce != null)
			{
				var user = _context.Klienti.Where(x => x.Id == _userManager.GetUserId(User)).SingleOrDefault();
				if (user != null)
				{
					return View((aukce, user.Zustatek));
				}
				return View((aukce, 0));
			}
			return View("Index", _context.Kategorie.ToList());
		}

		[HttpPost]
        public IActionResult Aukce([FromRoute] string id, [FromForm] int castka)
        {
            int AukceId = Int32.Parse(id);
            var aukce = _context.Aukce.Where(x => x.AukceId == AukceId).Include(x => x.Autor).FirstOrDefault();
            if (aukce != null)
            {
                var user = _context.Klienti.Where(x => x.Id == _userManager.GetUserId(User)).SingleOrDefault();
                if (user != null)
                {
					var zustatekZakaznika = user.Zustatek;
					var minimalniPrihoz = aukce.PrihozeniPoCastce ? aukce.MinimalniPrihoz : aukce.Cena * aukce.MinimalniPrihoz / 100;
					if (castka >= minimalniPrihoz && castka <= zustatekZakaznika)
					{
						user.Zustatek -= castka;
						aukce.Cena += castka;
						_context.Prihozy.Add(new Prihoz() { AukceId = aukce.AukceId, Castka = castka, NovaCena = aukce.Cena, Datum = DateTime.Now, KlientId = user.Id });
						_context.SaveChanges();
                    }
                    return View((aukce, zustatekZakaznika));
                }
                return View((aukce, 0));
            }
            return View("Index", _context.Kategorie.ToList());
        }

        public IActionResult Kategorie([FromRoute] string id)
		{
			int KategorieId = Int32.Parse(id);
			var kategorie = _context.Kategorie.Where(x => x.KategorieId == KategorieId).Include(x => x.Aukce).FirstOrDefault();
			if (kategorie != null)
			{
				return View(kategorie);
			}
			return View("Index", _context.Kategorie.ToList());
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}