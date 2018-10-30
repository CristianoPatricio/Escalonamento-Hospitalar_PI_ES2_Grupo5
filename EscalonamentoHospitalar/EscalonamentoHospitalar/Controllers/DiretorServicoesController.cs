using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EscalonamentoHospitalar.Data;
using EscalonamentoHospitalar.Models;

namespace EscalonamentoHospitalar.Controllers
{
    public class DiretorServicoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiretorServicoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DiretorServicoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DiretorServico.ToListAsync());
        }

        // GET: DiretorServicoes/Details/5
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

        // GET: DiretorServicoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DiretorServicoes/Create
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

        // GET: DiretorServicoes/Edit/5
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

        // POST: DiretorServicoes/Edit/5
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

        // GET: DiretorServicoes/Delete/5
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

        // POST: DiretorServicoes/Delete/5
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
