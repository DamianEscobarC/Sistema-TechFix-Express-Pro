using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema_TechFix_Express_Pro.Data;
using Sistema_TechFix_Express_Pro.Models;

namespace Sistema_TechFix_Express_Pro.Controllers
{
    public class OrdenesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdenesController(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task CargarListaDesplegableAsync(int? categoriaId = null, int? tecnicoId = null)
        {
            var categorias = await _context.Categorias.ToListAsync();
            var tecnicos = await _context.Tecnicos.ToListAsync();

            ViewBag.CategoriaId = new SelectList(categorias, "Id", "Nombre", categoriaId);
            ViewBag.TecnicoId = new SelectList(tecnicos, "Id", "NombreCompleto", tecnicoId);
        }
        //Get ordenes listado principal
        public async Task<IActionResult> Index()
        {
            var ordenes = await _context.OrdenesServicio
                .Include(o => o.Categoria)
                .Include(o => o.Tecnico)
                .ToListAsync();

            return View(ordenes);
        }
        //Get ordenes/create
        public async Task<IActionResult> Create()
        {
            await CargarListaDesplegableAsync();
            return View(new OrdenServicio { FechaIngreso = DateTime.Today, Estado = "Pendiente" });
        }
        //Post ordenes /CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrdenServicio orden)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orden);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await CargarListaDesplegableAsync(orden.CategoriaId, orden.TecnicoId);
            return View(orden);
        }

        //Get Ordenes/Edit 5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var orden = await _context.OrdenesServicio.FindAsync(id);
            if (orden == null) return NotFound();

            await CargarListaDesplegableAsync(orden.CategoriaId, orden.TecnicoId);
            return View(orden);
        }
        // POST: Ordenes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrdenServicio orden)
        {
            if (id != orden.Id) return NotFound();

            if (await _context.OrdenesServicio.AnyAsync(o => o.NumeroOrden == orden.NumeroOrden && o.Id != orden.Id))
            {
                ModelState.AddModelError("NumeroOrden", "Otro registro ya utiliza este número de orden.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.OrdenesServicio.AnyAsync(e => e.Id == orden.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            // Recarga defensiva obligatoria si falla la validación
            await CargarListaDesplegableAsync(orden.CategoriaId, orden.TecnicoId);
            return View(orden);
        }

        // GET: Ordenes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var orden = await _context.OrdenesServicio
                .Include(o => o.Categoria)
                .Include(o => o.Tecnico)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (orden == null) return NotFound();

            return View(orden);
        }

        // POST: Ordenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orden = await _context.OrdenesServicio.FindAsync(id);
            if (orden != null)
            {
                _context.OrdenesServicio.Remove(orden);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}