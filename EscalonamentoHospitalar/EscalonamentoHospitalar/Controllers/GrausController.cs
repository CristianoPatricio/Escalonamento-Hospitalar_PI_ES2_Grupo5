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
    public class GrausController : Controller
    {
        private readonly HospitalDbContext _context;

        public GrausController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Graus
        public async Task<IActionResult> Index()
        {
            return View(await _context.Grau.ToListAsync());
        }

        // GET: Graus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grau = await _context.Grau
                .FirstOrDefaultAsync(m => m.GrauId == id);
            if (grau == null)
            {
                return NotFound();
            }

            return View(grau);
        }

        // GET: Graus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Graus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GrauId,TipoGrau")] Grau grau)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grau);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grau);
        }

        // GET: Graus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grau = await _context.Grau.FindAsync(id);
            if (grau == null)
            {
                return NotFound();
            }
            return View(grau);
        }

        // POST: Graus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GrauId,TipoGrau")] Grau grau)
        {
            if (id != grau.GrauId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grau);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrauExists(grau.GrauId))
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
            return View(grau);
        }

        // GET: Graus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grau = await _context.Grau
                .FirstOrDefaultAsync(m => m.GrauId == id);
            if (grau == null)
            {
                return NotFound();
            }

            return View(grau);
        }

        // POST: Graus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grau = await _context.Grau.FindAsync(id);
            _context.Grau.Remove(grau);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrauExists(int id)
        {
            return _context.Grau.Any(e => e.GrauId == id);
        }
    }
}
