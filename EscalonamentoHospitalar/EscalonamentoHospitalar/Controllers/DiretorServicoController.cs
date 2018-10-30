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
    public class DiretorServicoController : Controller
    {
        private readonly HospitalDbContext _context;

        public DiretorServicoController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: DiretorServico
        public async Task<IActionResult> Index()
        {
            return View(await _context.DiretorServico.ToListAsync());
        }

        // GET: DiretorServico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diretorServico = await _context.DiretorServico
                .FirstOrDefaultAsync(m => m.DiretorServicoID == id);
            if (diretorServico == null)
            {
                return NotFound();
            }

            return View(diretorServico);
        }

        // GET: DiretorServico/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DiretorServico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiretorServicoID,Name,NumeroMecanografico,Contacto,Email,CC,Morada")] DiretorServico diretorServico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diretorServico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diretorServico);
        }

        // GET: DiretorServico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diretorServico = await _context.DiretorServico.FindAsync(id);
            if (diretorServico == null)
            {
                return NotFound();
            }
            return View(diretorServico);
        }

        // POST: DiretorServico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiretorServicoID,Name,NumeroMecanografico,Contacto,Email,CC,Morada")] DiretorServico diretorServico)
        {
            if (id != diretorServico.DiretorServicoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diretorServico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiretorServicoExists(diretorServico.DiretorServicoID))
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
            return View(diretorServico);
        }

        // GET: DiretorServico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diretorServico = await _context.DiretorServico
                .FirstOrDefaultAsync(m => m.DiretorServicoID == id);
            if (diretorServico == null)
            {
                return NotFound();
            }

            return View(diretorServico);
        }

        // POST: DiretorServico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diretorServico = await _context.DiretorServico.FindAsync(id);
            _context.DiretorServico.Remove(diretorServico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiretorServicoExists(int id)
        {
            return _context.DiretorServico.Any(e => e.DiretorServicoID == id);
        }
    }
}
