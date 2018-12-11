using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EscalonamentoHospitalar.Models;

namespace EscalonamentoHospitalar.Controllers
{
    public class EstadoPedidoTrocasController : Controller
    {
        private readonly HospitalDbContext _context;

        public EstadoPedidoTrocasController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: EstadoPedidoTrocas
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstadoPedidoTrocas.ToListAsync());
        }

        // GET: EstadoPedidoTrocas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoPedidoTroca = await _context.EstadoPedidoTrocas
                .FirstOrDefaultAsync(m => m.EstadoPedidoTrocaId == id);
            if (estadoPedidoTroca == null)
            {
                return NotFound();
            }

            return View(estadoPedidoTroca);
        }

        // GET: EstadoPedidoTrocas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadoPedidoTrocas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstadoPedidoTrocaId,Nome")] EstadoPedidoTroca estadoPedidoTroca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadoPedidoTroca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadoPedidoTroca);
        }

        // GET: EstadoPedidoTrocas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoPedidoTroca = await _context.EstadoPedidoTrocas.FindAsync(id);
            if (estadoPedidoTroca == null)
            {
                return NotFound();
            }
            return View(estadoPedidoTroca);
        }

        // POST: EstadoPedidoTrocas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstadoPedidoTrocaId,Nome")] EstadoPedidoTroca estadoPedidoTroca)
        {
            if (id != estadoPedidoTroca.EstadoPedidoTrocaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadoPedidoTroca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoPedidoTrocaExists(estadoPedidoTroca.EstadoPedidoTrocaId))
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
            return View(estadoPedidoTroca);
        }

        // GET: EstadoPedidoTrocas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoPedidoTroca = await _context.EstadoPedidoTrocas
                .FirstOrDefaultAsync(m => m.EstadoPedidoTrocaId == id);
            if (estadoPedidoTroca == null)
            {
                return NotFound();
            }

            return View(estadoPedidoTroca);
        }

        // POST: EstadoPedidoTrocas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadoPedidoTroca = await _context.EstadoPedidoTrocas.FindAsync(id);
            _context.EstadoPedidoTrocas.Remove(estadoPedidoTroca);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoPedidoTrocaExists(int id)
        {
            return _context.EstadoPedidoTrocas.Any(e => e.EstadoPedidoTrocaId == id);
        }
    }
}
