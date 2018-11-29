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
    public class PatologiasController : Controller
    {
        private readonly HospitalDbContext _context;

        public PatologiasController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Patologias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Patologia.ToListAsync());
        }

        // GET: Patologias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patologia = await _context.Patologia
                .FirstOrDefaultAsync(m => m.PatologiaId == id);
            if (patologia == null)
            {
                return NotFound();
            }

            return View(patologia);
        }

        // GET: Patologias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patologias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatologiaId,Nome")] Patologia patologia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patologia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            
            return View(patologia);
        }



        // GET: Patologias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patologia = await _context.Patologia.FindAsync(id);
            if (patologia == null)
            {
                return NotFound();
            }
            return View(patologia);
        }

        // POST: Patologias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatologiaId,Nome")] Patologia patologia)
        {
            if (id != patologia.PatologiaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patologia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatologiaExists(patologia.PatologiaId))
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
            return View(patologia);
        }

        // GET: Patologias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patologia = await _context.Patologia
                .FirstOrDefaultAsync(m => m.PatologiaId == id);
            if (patologia == null)
            {
                return NotFound();
            }

            return View(patologia);
        }

        // POST: Patologias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patologia = await _context.Patologia.FindAsync(id);
            _context.Patologia.Remove(patologia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatologiaExists(int id)
        {
            return _context.Patologia.Any(e => e.PatologiaId == id);
        }


                         
        
    }
}
