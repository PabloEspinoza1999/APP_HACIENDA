using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EV_HACIENDA.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EV_HACIENDA.Controllers
{
    public class EmisorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmisorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmisorController
        public IActionResult Index()
        {
            var emisores = _context.Emisores.ToList();
            return View(emisores);
        }

        // GET: EmisorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var emisor = await _context.Emisores.FindAsync(id);
            if (emisor == null)
            {
                return NotFound();
            }
            return View(emisor);
        }

        // GET: EmisorController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmisorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmCreate([Bind("Nombre, Identificacion, CorreoElectronico, Provincia, Canton, Distrito, Barrio, OtrasSenas, Telefono, CodigoPais")] Emisor emisor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emisor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emisor);
        }

        // GET: EmisorController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var emisor = await _context.Emisores.FindAsync(id);
            if (emisor == null)
            {
                return NotFound();
            }
            return View(emisor);
        }

        // POST: EmisorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmisorId, Nombre, Identificacion, CorreoElectronico, Provincia, Canton, Distrito, Barrio, OtrasSenas, Telefono, CodigoPais")] Emisor emisor)
        {
            if (id != emisor.EmisorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emisor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmisorExists(emisor.EmisorId))
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
            return View(emisor);
        }

        // GET: EmisorController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var emisor = await _context.Emisores.FindAsync(id);
            if (emisor == null)
            {
                return NotFound();
            }
            return View(emisor);
        }

        // POST: EmisorController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emisor = await _context.Emisores.FindAsync(id);
            if (emisor != null)
            {
                _context.Emisores.Remove(emisor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EmisorExists(int id)
        {
            return _context.Emisores.Any(e => e.EmisorId == id);
        }
    }
}
