using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EV_HACIENDA.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EV_HACIENDA.Controllers
{
    public class ReceptorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceptorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReceptorController
        public IActionResult Index()
        {
            var receptores = _context.Receptores.ToList();
            return View(receptores);
        }

        // GET: ReceptorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var receptor = await _context.Receptores.FindAsync(id);
            if (receptor == null)
            {
                return NotFound();
            }
            return View(receptor);
        }

        // GET: ReceptorController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReceptorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmCreate([Bind("Nombre, Identificacion, CorreoElectronico, Provincia, Canton, Distrito, Barrio, OtrasSenas, Telefono, CodigoPais")] Receptor receptor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receptor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(receptor);
        }

        // GET: ReceptorController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var receptor = await _context.Receptores.FindAsync(id);
            if (receptor == null)
            {
                return NotFound();
            }
            return View(receptor);
        }

        // POST: ReceptorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReceptorId, Nombre, Identificacion, CorreoElectronico, Provincia, Canton, Distrito, Barrio, OtrasSenas, Telefono, CodigoPais")] Receptor receptor)
        {
            if (id != receptor.ReceptorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receptor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptorExists(receptor.ReceptorId))
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
            return View(receptor);
        }

        // GET: ReceptorController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var receptor = await _context.Receptores.FindAsync(id);
            if (receptor == null)
            {
                return NotFound();
            }
            return View(receptor);
        }

        // POST: ReceptorController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receptor = await _context.Receptores.FindAsync(id);
            if (receptor != null)
            {
                _context.Receptores.Remove(receptor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ReceptorExists(int id)
        {
            return _context.Receptores.Any(e => e.ReceptorId == id);
        }
    }
}
