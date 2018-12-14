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
    [Authorize(Policy = "AcessoRestritoDiretorServico")]  // Política de acesso restrito ao Diretor de Serviço 
    public class EspecialidadeMedicosController : Controller
    {
        private readonly HospitalDbContext _context;

        public EspecialidadeMedicosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: EspecialidadeMedicos
        public async Task<IActionResult> Index()
        {
            return View(await _context.EspecialidadeMedicos.ToListAsync());
        }

        // GET: EspecialidadeMedicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidadeMedico = await _context.EspecialidadeMedicos
                .FirstOrDefaultAsync(m => m.EspecialidadeMedicoId == id);
            if (especialidadeMedico == null)
            {
                return NotFound();
            }

            return View(especialidadeMedico);
        }

        // GET: EspecialidadeMedicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EspecialidadeMedicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EspecialidadeMedicoId,NomeEspecialidade")] EspecialidadeMedico especialidadeMedico)
        {
            var nomeEspecialidade = especialidadeMedico.NomeEspecialidade;

            //Validar Nome Especialidade
            if (NomeEspecialidadeIsInvalid(nomeEspecialidade) == true)
            {
                //Mensagem de erro se a especialidade já existir
                ModelState.AddModelError("NomeEspecialidade", "Esta especialidade já existe");
            }

            if (ModelState.IsValid)
            {
                if (!NomeEspecialidadeIsInvalid(nomeEspecialidade))
                {
                    _context.Add(especialidadeMedico);
                    await _context.SaveChangesAsync();
                    TempData["successInsertEspecialidade"] = "Especialidade inserida com sucesso";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(especialidadeMedico);
        }

        // GET: EspecialidadeMedicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidadeMedico = await _context.EspecialidadeMedicos.FindAsync(id);
            if (especialidadeMedico == null)
            {
                return NotFound();
            }
            return View(especialidadeMedico);
        }

        // POST: EspecialidadeMedicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EspecialidadeMedicoId,NomeEspecialidade")] EspecialidadeMedico especialidadeMedico)
        {
            if (id != especialidadeMedico.EspecialidadeMedicoId)
            {
                return NotFound();
            }

            var nomeEspecialidade = especialidadeMedico.NomeEspecialidade;
            var idEspecialidade = especialidadeMedico.EspecialidadeMedicoId;

            //Validar Especialidade
            if (NomeEspecialidadeIsInvalidEdit(nomeEspecialidade, idEspecialidade) == true)
            {
                //Mensagem de erro se a especialidade já existir
                ModelState.AddModelError("NomeEspecialidade", "Esta especialidade já existe");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!NomeEspecialidadeIsInvalidEdit(nomeEspecialidade, idEspecialidade))
                    {
                        _context.Update(especialidadeMedico);
                        await _context.SaveChangesAsync();
                        TempData["successEdit"] = "Especialidade alterada com sucesso";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecialidadeMedicoExists(especialidadeMedico.EspecialidadeMedicoId))
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
            return View(especialidadeMedico);
        }

        // GET: EspecialidadeMedicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidadeMedico = await _context.EspecialidadeMedicos
                .FirstOrDefaultAsync(m => m.EspecialidadeMedicoId == id);
            if (especialidadeMedico == null)
            {
                return NotFound();
            }

            return View(especialidadeMedico);
        }

        // POST: EspecialidadeMedicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var especialidadeMedico = await _context.EspecialidadeMedicos.FindAsync(id);

            if (EspecialidadeContainsMedicos(id))
            {
                TempData["errorDelete"] = "IMPOSSIVEL ELIMINAR";
                // voltar para a mesma página
            }

            if (!EspecialidadeContainsMedicos(id))
            {
                _context.EspecialidadeMedicos.Remove(especialidadeMedico);
                await _context.SaveChangesAsync();
                TempData["successDelete"] = "Registo eliminado com sucesso";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EspecialidadeMedicoExists(int id)
        {
            return _context.EspecialidadeMedicos.Any(e => e.EspecialidadeMedicoId == id);
        }

        /************************Funções auxiliares************************/
        /**
        * @param especialidade
        * @return true if the especialidade already exist in DB
        */
        private bool NomeEspecialidadeIsInvalid(string especialidade)
        {
            bool IsInvalid = false;

            //Procura na BD se existem especialidades com o mesmo nome
            var especialidades = from m in _context.EspecialidadeMedicos
                                 where m.NomeEspecialidade == especialidade
                                 select m;

            if (!especialidades.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        private bool EspecialidadeContainsMedicos(int idEspecialidade)
        {
            bool IsInvalid = false;

            //Procura na BD dos Enfermeiros se há enfermeiros que têm essa especialidade
            var medicoContainsEspecialidade = from m in _context.Medicos
                                 where m.EspecialidadeMedicoId == idEspecialidade
                                 select m;

            if (!medicoContainsEspecialidade.Count().Equals(0))
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
        private bool NomeEspecialidadeIsInvalidEdit(string especialidade, int id)
        {
            bool IsInvalid = false;

            //Procura na BD se existem especialidades com o mesmo nome
            var especialidades = from m in _context.EspecialidadeMedicos
                                 where m.NomeEspecialidade == especialidade && m.EspecialidadeMedicoId != id
                                 select m;

            if (!especialidades.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }
    }
}