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
    public class RegrasController : Controller
    {
        private readonly HospitalDbContext _context;

        public RegrasController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Regras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Regras.ToListAsync());
        }

        // GET: Regras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regras = await _context.Regras
                .FirstOrDefaultAsync(m => m.RegrasId == id);
            if (regras == null)
            {
                return NotFound();
            }

            return View(regras);
        }

        // GET: Regras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Regras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegrasId,RegrasEscalonamento")] Regras regras)
        {
            if (ModelState.IsValid)
            {
                _context.Add(regras);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(regras);
        }

        // GET: Regras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regras = await _context.Regras.FindAsync(id);
            if (regras == null)
            {
                return NotFound();
            }
            return View(regras);
        }

        // POST: Regras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegrasId,RegrasEscalonamento")] Regras regras)
        {
            if (id != regras.RegrasId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(regras);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegrasExists(regras.RegrasId))
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
            return View(regras);
        }

        // GET: Regras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regras = await _context.Regras
                .FirstOrDefaultAsync(m => m.RegrasId == id);
            if (regras == null)
            {
                return NotFound();
            }

            return View(regras);
        }

        // POST: Regras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var regras = await _context.Regras.FindAsync(id);
            _context.Regras.Remove(regras);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegrasExists(int id)
        {
            return _context.Regras.Any(e => e.RegrasId == id);
        }
    }
}
