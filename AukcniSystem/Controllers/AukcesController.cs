using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AukcniSystem.Data;
using AukcniSystem.Models;

namespace AukcniSystem.Controllers
{
    public class AukcesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AukcesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Aukces
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Aukce.Include(a => a.Autor).Include(a => a.Kategorie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Aukces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Aukce == null)
            {
                return NotFound();
            }

            var aukce = await _context.Aukce
                .Include(a => a.Autor)
                .Include(a => a.Kategorie)
                .FirstOrDefaultAsync(m => m.AukceId == id);
            if (aukce == null)
            {
                return NotFound();
            }

            return View(aukce);
        }

        // GET: Aukces/Create
        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(_context.Klienti, "Id", "Id");
            ViewData["KategorieId"] = new SelectList(_context.Kategorie, "KategorieId", "KategorieId");
            return View();
        }

        // POST: Aukces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AukceId,AutorId,KategorieId,Nazev,Popis,Cena,Foto,Datum,PrihozeniPoCastce,MinimalniPrihoz,Schvalena,DobaTrvani")] Aukce aukce)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aukce);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Klienti, "Id", "Id", aukce.AutorId);
            ViewData["KategorieId"] = new SelectList(_context.Kategorie, "KategorieId", "KategorieId", aukce.KategorieId);
            return View(aukce);
        }

        // GET: Aukces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Aukce == null)
            {
                return NotFound();
            }

            var aukce = await _context.Aukce.FindAsync(id);
            if (aukce == null)
            {
                return NotFound();
            }
            ViewData["AutorId"] = new SelectList(_context.Klienti, "Id", "Id", aukce.AutorId);
            ViewData["KategorieId"] = new SelectList(_context.Kategorie, "KategorieId", "KategorieId", aukce.KategorieId);
            return View(aukce);
        }

        // POST: Aukces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AukceId,AutorId,KategorieId,Nazev,Popis,Cena,Foto,Datum,PrihozeniPoCastce,MinimalniPrihoz,Schvalena,DobaTrvani")] Aukce aukce)
        {
            if (id != aukce.AukceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aukce);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AukceExists(aukce.AukceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Klienti, "Id", "Id", aukce.AutorId);
            ViewData["KategorieId"] = new SelectList(_context.Kategorie, "KategorieId", "KategorieId", aukce.KategorieId);
            return View(aukce);
        }

        // GET: Aukces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Aukce == null)
            {
                return NotFound();
            }

            var aukce = await _context.Aukce
                .Include(a => a.Autor)
                .Include(a => a.Kategorie)
                .FirstOrDefaultAsync(m => m.AukceId == id);
            if (aukce == null)
            {
                return NotFound();
            }

            return View(aukce);
        }

        // POST: Aukces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Aukce == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Aukce'  is null.");
            }
            var aukce = await _context.Aukce.FindAsync(id);
            if (aukce != null)
            {
                _context.Aukce.Remove(aukce);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AukceExists(int id)
        {
          return (_context.Aukce?.Any(e => e.AukceId == id)).GetValueOrDefault();
        }
    }
}
