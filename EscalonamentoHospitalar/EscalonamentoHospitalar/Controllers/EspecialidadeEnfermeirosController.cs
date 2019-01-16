using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EscalonamentoHospitalar.Models;
using Microsoft.AspNetCore.Authorization;

namespace EscalonamentoHospitalar.Controllers
{
    [Authorize]  
    public class EspecialidadeEnfermeirosController : Controller
    {
        private readonly HospitalDbContext _context;

        public EspecialidadeEnfermeirosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: EspecialidadeEnfermeiros
        public async Task<IActionResult> Index()
        {
            return View(await _context.EspecialidadesEnfermeiros.ToListAsync());
        }

        // GET: EspecialidadeEnfermeiros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidadeEnfermeiro = await _context.EspecialidadesEnfermeiros
                .FirstOrDefaultAsync(m => m.EspecialidadeEnfermeiroId == id);
            if (especialidadeEnfermeiro == null)
            {
                return NotFound();
            }

            return View(especialidadeEnfermeiro);
        }

        // GET: EspecialidadeEnfermeiros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EspecialidadeEnfermeiros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EspecialidadeEnfermeiroId,Especialidade")] EspecialidadeEnfermeiro especialidadeEnfermeiro)
        {
            var nomeEspecialidade = especialidadeEnfermeiro.Especialidade;

            //Validar Nome Especialidade
            if (EspecialidadeIsInvalid(nomeEspecialidade) == true)
            {
                //Mensagem de erro se a especialidade já existir
                ModelState.AddModelError("Especialidade", "Esta especialidade já existe");
            }

            if (ModelState.IsValid)
            {
                if (!EspecialidadeIsInvalid(nomeEspecialidade))
                {
                    _context.Add(especialidadeEnfermeiro);
                    await _context.SaveChangesAsync();
                    TempData["successInsertEspecialidade"] = "Especialidade inserida com sucesso";
                    return RedirectToAction(nameof(Index));
                }               
            }
            return View(especialidadeEnfermeiro);
        }

        // GET: EspecialidadeEnfermeiros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidadeEnfermeiro = await _context.EspecialidadesEnfermeiros.FindAsync(id);
            if (especialidadeEnfermeiro == null)
            {
                return NotFound();
            }
            return View(especialidadeEnfermeiro);
        }

        // POST: EspecialidadeEnfermeiros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EspecialidadeEnfermeiroId,Especialidade")] EspecialidadeEnfermeiro especialidadeEnfermeiro)
        {
            if (id != especialidadeEnfermeiro.EspecialidadeEnfermeiroId)
            {
                return NotFound();
            }

            var nomeEspecialidade = especialidadeEnfermeiro.Especialidade;
            var idEsp = especialidadeEnfermeiro.EspecialidadeEnfermeiroId;

            //Validar Especialidade
            if (EspecialidadeIsInvalidEdit(nomeEspecialidade, idEsp) == true)
            {
                //Mensagem de erro se a especialidade já existir
                ModelState.AddModelError("Especialidade", "Esta especialidade já existe");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!EspecialidadeIsInvalidEdit(nomeEspecialidade, idEsp))
                    {
                        _context.Update(especialidadeEnfermeiro);
                        await _context.SaveChangesAsync();
                        TempData["successEdit"] = "Especialidade alterada com sucesso";
                    }                 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecialidadeEnfermeiroExists(especialidadeEnfermeiro.EspecialidadeEnfermeiroId))
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
            return View(especialidadeEnfermeiro);
        }

        // GET: EspecialidadeEnfermeiros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidadeEnfermeiro = await _context.EspecialidadesEnfermeiros
                .FirstOrDefaultAsync(m => m.EspecialidadeEnfermeiroId == id);
            if (especialidadeEnfermeiro == null)
            {
                return NotFound();
            }

            return View(especialidadeEnfermeiro);
        }

        // POST: EspecialidadeEnfermeiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var especialidadeEnfermeiro = await _context.EspecialidadesEnfermeiros.FindAsync(id);

            if (EspecialidadeContainsEnfermeiros(id))
            {
                TempData["errorDelete"] = "IMPOSSIVEL ELIMINAR";
            }

            if (!EspecialidadeContainsEnfermeiros(id))
            {
                _context.EspecialidadesEnfermeiros.Remove(especialidadeEnfermeiro);
                await _context.SaveChangesAsync();
                TempData["successDelete"] = "Registo eliminado com sucesso";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));

        }

        private bool EspecialidadeEnfermeiroExists(int id)
        {
            return _context.EspecialidadesEnfermeiros.Any(e => e.EspecialidadeEnfermeiroId == id);
        }

        /************************Funções auxiliares************************/
        /**
        * @param especialidade
        * @return true if the especialidade already exist in DB
        */
        private bool EspecialidadeIsInvalid(string especialidade)
        {
            bool IsInvalid = false;

            //Procura na BD se existem especialidades com o mesmo nome
            var especialidades = from e in _context.EspecialidadesEnfermeiros
                              where e.Especialidade == especialidade
                              select e;

            if (!especialidades.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        private bool EspecialidadeContainsEnfermeiros(int idEsp)
        {
            bool IsInvalid = false;

            //Procura na BD dos Enfermeiros se há enfermeiros que têm essa especialidade
            var enfContainsEsp = from e in _context.Enfermeiros
                                 where e.EspecialidadeEnfermeiroId == idEsp
                                 select e;

            if (!enfContainsEsp.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        /************************Edit***************************/
        /**
       * @param especialidade
       * @return true if the especialidade already exist in DB
       */
        private bool EspecialidadeIsInvalidEdit(string especialidade, int id)
        {
            bool IsInvalid = false;

            //Procura na BD se existem especialidades com o mesmo nome
            var especialidades = from e in _context.EspecialidadesEnfermeiros
                                 where e.Especialidade == especialidade && e.EspecialidadeEnfermeiroId != id
                                 select e;

            if (!especialidades.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }
    }
}
