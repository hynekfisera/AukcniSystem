using AukcniSystem.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AukcniSystem.Hubs
{
    public class AukceHub : Hub
	{
		private readonly ApplicationDbContext _context;
		public AukceHub(ApplicationDbContext context)
		{
			_context = context;
		}

		public void Prihoz(int aukceId, double zustatek, double castka)
		{
			var aukce = _context.Aukce.Where(x => x.AukceId == aukceId).Include(x => x.Autor).FirstOrDefault();
			if (aukce != null)
			{
				var minimalniPrihoz = aukce.PrihozeniPoCastce ? aukce.MinimalniPrihoz : aukce.Cena * aukce.MinimalniPrihoz / 100;
				if (castka >= minimalniPrihoz && castka <= zustatek)
				{
					Clients.All.SendAsync("Aktualizovat", aukce.Cena + castka);
				}
			}
		}
	}
}
