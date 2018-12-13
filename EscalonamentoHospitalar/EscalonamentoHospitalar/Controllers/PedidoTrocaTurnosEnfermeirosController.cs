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
    public class PedidoTrocaTurnosEnfermeirosController : Controller
    {
        private readonly HospitalDbContext _context;

        public PedidoTrocaTurnosEnfermeirosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: PedidoTrocaTurnosEnfermeiros
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.PedidoTrocaTurnosEnfermeiros
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.EnfermeiroRequerente)
                .Include(p => p.HorarioATrocarEnfermeiro)
                .Include(p => p.HorarioParaTrocaEnfermeiro);

            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: PedidoTrocaTurnosEnfermeiros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.EnfermeiroRequerente)
                .Include(p => p.HorarioATrocarEnfermeiro)
                .Include(p => p.HorarioParaTrocaEnfermeiro)
                .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosEnfermeiroId == id);

            if (pedidoTrocaTurnosEnfermeiro == null)
            {
                return NotFound();
            }

            return View(pedidoTrocaTurnosEnfermeiro);
        }

        // GET: PedidoTrocaTurnosEnfermeiros/Create
        public IActionResult Create()
        {
            ViewData["EnfermeiroRequerenteId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "Nome");
            ViewData["HorarioATrocarId"] = new SelectList(_context.HorarioATrocarEnfermeiros, "HorarioATrocarId", "HorarioATrocarId");
            ViewData["HorarioParaTrocaId"] = new SelectList(_context.Enfermeiros, "HorarioParaTrocaId", "HorarioParaTrocaId");
            ViewData["EstadoPedidoTrocaId"] = new SelectList(_context.EstadoPedidoTrocas, "EstadoPedidoTrocaId", "Nome");
            return View();
        }

        // POST: PedidoTrocaTurnosEnfermeiros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PedidoTrocaTurnosEnfermeiroId,DataPedido,EnfermeiroRequerenteId,HorarioATrocarId,HorarioParaTrocaId,EstadoPedidoTrocaId")] PedidoTrocaTurnosEnfermeiro pedidoTrocaTurnosEnfermeiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidoTrocaTurnosEnfermeiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnfermeiroRequerenteId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "Nome", pedidoTrocaTurnosEnfermeiro.EnfermeiroRequerenteId);
            ViewData["HorarioATrocarId"] = new SelectList(_context.HorarioATrocarEnfermeiros, "HorarioATrocarId", "HorarioATrocarId", pedidoTrocaTurnosEnfermeiro.HorarioATrocarId);
            ViewData["HorarioParaTrocaId"] = new SelectList(_context.Enfermeiros, "HorarioParaTrocaId", "HorarioParaTrocaId", pedidoTrocaTurnosEnfermeiro.HorarioParaTrocaId);
            ViewData["EstadoPedidoTrocaId"] = new SelectList(_context.EstadoPedidoTrocas, "EstadoPedidoTrocaId", "EstadoPedidoTrocaId", pedidoTrocaTurnosEnfermeiro.EstadoPedidoTrocaId);
            return View(pedidoTrocaTurnosEnfermeiro);
        }

        // GET: PedidoTrocaTurnosEnfermeiros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros.FindAsync(id);
            if (pedidoTrocaTurnosEnfermeiro == null)
            {
                return NotFound();
            }
            ViewData["EnfermeiroRequerenteId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "Nome");
            ViewData["HorarioATrocarId"] = new SelectList(_context.HorarioATrocarEnfermeiros, "HorarioATrocarId", "HorarioATrocarId");
            ViewData["HorarioParaTrocaId"] = new SelectList(_context.Enfermeiros, "HorarioParaTrocaId", "HorarioParaTrocaId");
            ViewData["EstadoPedidoTrocaId"] = new SelectList(_context.EstadoPedidoTrocas, "EstadoPedidoTrocaId", "EstadoPedidoTrocaId", pedidoTrocaTurnosEnfermeiro.EstadoPedidoTrocaId);
            return View(pedidoTrocaTurnosEnfermeiro);
        }

        // POST: PedidoTrocaTurnosEnfermeiros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PedidoTrocaTurnosEnfermeiroId,DataPedido,EnfermeiroRequerenteId,HorarioATrocarId,HorarioParaTrocaId,EstadoPedidoTrocaId")] PedidoTrocaTurnosEnfermeiro pedidoTrocaTurnosEnfermeiro)
        {
            if (id != pedidoTrocaTurnosEnfermeiro.PedidoTrocaTurnosEnfermeiroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidoTrocaTurnosEnfermeiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoTrocaTurnosEnfermeiroExists(pedidoTrocaTurnosEnfermeiro.PedidoTrocaTurnosEnfermeiroId))
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
            ViewData["EnfermeiroRequerenteId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "Nome", pedidoTrocaTurnosEnfermeiro.EnfermeiroRequerenteId);
            ViewData["HorarioATrocarId"] = new SelectList(_context.HorarioATrocarEnfermeiros, "HorarioATrocarId", "HorarioATrocarId", pedidoTrocaTurnosEnfermeiro.HorarioATrocarId);
            ViewData["HorarioParaTrocaId"] = new SelectList(_context.Enfermeiros, "HorarioParaTrocaId", "HorarioParaTrocaId", pedidoTrocaTurnosEnfermeiro.HorarioParaTrocaId);
            ViewData["EstadoPedidoTrocaId"] = new SelectList(_context.EstadoPedidoTrocas, "EstadoPedidoTrocaId", "EstadoPedidoTrocaId", pedidoTrocaTurnosEnfermeiro.EstadoPedidoTrocaId);
            return View(pedidoTrocaTurnosEnfermeiro);
        }

        // GET: PedidoTrocaTurnosEnfermeiros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.EnfermeiroRequerente)
                .Include(p => p.HorarioATrocarEnfermeiro)
                .Include(p => p.HorarioParaTrocaEnfermeiro)
                .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosEnfermeiroId == id);
            if (pedidoTrocaTurnosEnfermeiro == null)
            {
                return NotFound();
            }

            return View(pedidoTrocaTurnosEnfermeiro);
        }

        // POST: PedidoTrocaTurnosEnfermeiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros.FindAsync(id);
            _context.PedidoTrocaTurnosEnfermeiros.Remove(pedidoTrocaTurnosEnfermeiro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoTrocaTurnosEnfermeiroExists(int id)
        {
            return _context.PedidoTrocaTurnosEnfermeiros.Any(e => e.PedidoTrocaTurnosEnfermeiroId == id);
        }
    }
}
