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
    public class EnfermeirosController : Controller
    {
        private readonly HospitalDbContext _context;

        public EnfermeirosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Enfermeiros
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.Enfermeiros.Include(e => e.EspecialidadeEnfermeiro);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: Enfermeiros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiro = await _context.Enfermeiros
                .Include(e => e.EspecialidadeEnfermeiro)
                .FirstOrDefaultAsync(m => m.EnfermeiroId == id);
            if (enfermeiro == null)
            {
                return NotFound();
            }

            return View(enfermeiro);
        }

        // GET: Enfermeiros/Create
        public IActionResult Create()
        {
                                                                                                                                         //Apresenta Nome da Especialidade 
            ViewData["EspecialidadeEnfermeiroId"] = new SelectList(_context.Set<EspecialidadeEnfermeiro>(), "EspecialidadeEnfermeiroId", "Especialidade");
            return View();
        }

        // POST: Enfermeiros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnfermeiroId,NumeroMecanografico,Nome,EspecialidadeEnfermeiroId,Contacto,Email,Data_Nascimento,CC,Filhos,Data_Nascimento_Filho")] Enfermeiro enfermeiro)
        {
            if (ModelState.IsValid)
            {
                // TODO fazer restantes validações
                //Verificar se a Data de Nascimento está correta (tem de ter no minimo 22 anos)
                if (!((DateTime.Today.Year - enfermeiro.Data_Nascimento.Year) <= 22 && (DateTime.Today.Year - enfermeiro.Data_Nascimento.Year) <= 0))
                {
                    _context.Add(enfermeiro);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["EspecialidadeEnfermeiroId"] = new SelectList(_context.Set<EspecialidadeEnfermeiro>(), "EspecialidadeEnfermeiroId", "Especialidade", enfermeiro.EspecialidadeEnfermeiroId);
            return View(enfermeiro);
        }

        // GET: Enfermeiros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiro = await _context.Enfermeiros.FindAsync(id);
            if (enfermeiro == null)
            {
                return NotFound();
            }
            ViewData["EspecialidadeEnfermeiroId"] = new SelectList(_context.Set<EspecialidadeEnfermeiro>(), "EspecialidadeEnfermeiroId", "Especialidade", enfermeiro.EspecialidadeEnfermeiroId);
            return View(enfermeiro);
        }

        // POST: Enfermeiros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnfermeiroId,NumeroMecanografico,Nome,EspecialidadeEnfermeiroId,Contacto,Email,Data_Nascimento,CC,Filhos,Data_Nascimento_Filho")] Enfermeiro enfermeiro)
        {
            if (id != enfermeiro.EnfermeiroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enfermeiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnfermeiroExists(enfermeiro.EnfermeiroId))
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
            ViewData["EspecialidadeEnfermeiroId"] = new SelectList(_context.Set<EspecialidadeEnfermeiro>(), "EspecialidadeEnfermeiroId", "Especialidade", enfermeiro.EspecialidadeEnfermeiroId);
            return View(enfermeiro);
        }

        // GET: Enfermeiros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiro = await _context.Enfermeiros
                .Include(e => e.EspecialidadeEnfermeiro)
                .FirstOrDefaultAsync(m => m.EnfermeiroId == id);
            if (enfermeiro == null)
            {
                return NotFound();
            }

            return View(enfermeiro);
        }

        // POST: Enfermeiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enfermeiro = await _context.Enfermeiros.FindAsync(id);
            _context.Enfermeiros.Remove(enfermeiro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnfermeiroExists(int id)
        {
            return _context.Enfermeiros.Any(e => e.EnfermeiroId == id);
        }
    }
}
