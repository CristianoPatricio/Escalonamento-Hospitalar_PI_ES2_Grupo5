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
    public class TratamentosController : Controller
    {
        private readonly HospitalDbContext _context;

        public TratamentosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Tratamentos
        public async Task<IActionResult> Index()
        {

            var hospitalDbContext = _context.Tratamentos.Include(t => t.Grau).Include(t => t.Medico).Include(t => t.Paciente).Include(t => t.Patologia).Include(t => t.Regime).Include(t =>t.Estado);

            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: Tratamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamento = await _context.Tratamentos
                .Include(t => t.Grau)
                .Include(t => t.Medico)
                .Include(t => t.Paciente)
                .Include(t => t.Patologia)
                .Include(t => t.Regime)
                .Include(t => t.Estado)
                .FirstOrDefaultAsync(m => m.TratamentoId == id);
            if (tratamento == null)
            {
                return NotFound();
            }

            return View(tratamento);
        }

        // GET: Tratamentos/Create
        public IActionResult Create()
        {
            ViewData["GrauId"] = new SelectList(_context.Grau, "GrauId", "TipoGrau");
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nome");
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nome");
            ViewData["PatologiaId"] = new SelectList(_context.Patologia, "PatologiaId", "Nome");
            ViewData["RegimeId"] = new SelectList(_context.Regime, "RegimeId", "TipoRegime");
            //ViewData["EstadoId"] = new SelectList(_context.Estado, "EstadoId", "Nome");

            return View();
        }

        // POST: Tratamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TratamentoId,PatologiaId,PacienteId,GrauId,DataInicio,DataFim,DuracaoCiclo,RegimeId,MedicoId")] Tratamento tratamento)
        {
            /*********Validações***********/

            DateTime dateNow = DateTime.Now;
            DateTime inicioTratamento = tratamento.DataInicio;
            DateTime fimTratamento = tratamento.DataFim;
            int tratamentoId = tratamento.TratamentoId;
            int patologiaId = tratamento.PatologiaId;
            int pacienteId = tratamento.PacienteId;
            int grauId = tratamento.GrauId;
            DateTime DataInicio = tratamento.DataInicio;
            DateTime DataFim = tratamento.DataFim;
            string DuracaoCiclo = tratamento.DuracaoCiclo;
            int RegimeId = tratamento.RegimeId;
            int MedicoId = tratamento.MedicoId;
            


            Estado estadoTratamento = _context.Estado.SingleOrDefault(e => e.Nome == "Em Espera");

            /*bool dataTratamentoIsInvalid = false;

            if(DataTratamentoIsInvalid(dateNow,inicioTratamento) == true)
            {
                dataTratamentoIsInvalid = true;
                ModelState.AddModelError("DataInicio", "Data Inválida, não pode ser inferior à data atual");
            }
            */

            if (ModelState.IsValid)
            {
                _context.Add(
                    
                    new Tratamento {TratamentoId = tratamentoId, EstadoId = estadoTratamento.EstadoId }
                    
                    );
                await _context.SaveChangesAsync();
                TempData["notice"] = "Tratamento inserido com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["GrauId"] = new SelectList(_context.Grau, "GrauId", "TipoGrau", tratamento.GrauId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nome", tratamento.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nome", tratamento.PacienteId);
            ViewData["PatologiaId"] = new SelectList(_context.Patologia, "PatologiaId", "Nome", tratamento.PatologiaId);
            return View(tratamento);
        }

        // GET: Tratamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamento = await _context.Tratamentos.FindAsync(id);
            if (tratamento == null)
            {
                return NotFound();
            }
            ViewData["GrauId"] = new SelectList(_context.Grau, "GrauId", "TipoGrau", tratamento.GrauId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nome", tratamento.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nome", tratamento.PacienteId);
            ViewData["PatologiaId"] = new SelectList(_context.Patologia, "PatologiaId", "Nome", tratamento.PatologiaId);
            ViewData["RegimeId"] = new SelectList(_context.Regime, "RegimeId", "TipoRegime", tratamento.RegimeId);
            ViewData["EstadoId"] = new SelectList(_context.Estado, "EstadoId", "Nome", tratamento.EstadoId);
            return View(tratamento);
        }

        // POST: Tratamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TratamentoId,PatologiaId,PacienteId,GrauId,DataInicio,DataFim,DuracaoCiclo,RegimeId,EstadoId,MedicoId")] Tratamento tratamento)
        {
            if (id != tratamento.TratamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tratamento);
                    await _context.SaveChangesAsync();
                    TempData["successEdit"] = "Registo alterado com sucesso";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TratamentoExists(tratamento.TratamentoId))
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
            ViewData["GrauId"] = new SelectList(_context.Grau, "GrauId", "TipoGrau", tratamento.GrauId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nome", tratamento.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nome", tratamento.PacienteId);
            ViewData["PatologiaId"] = new SelectList(_context.Patologia, "PatologiaId", "Nome", tratamento.PatologiaId);
            ViewData["RegimeId"] = new SelectList(_context.Regime, "RegimeId", "TipoRegime", tratamento.RegimeId);
            ViewData["EstadoId"] = new SelectList(_context.Estado, "EstadoId", "Nome", tratamento.EstadoId);
            return View(tratamento);
        }

        // GET: Tratamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamento = await _context.Tratamentos
                .Include(t => t.Grau)
                .Include(t => t.Medico)
                .Include(t => t.Paciente)
                .Include(t => t.Patologia)
                .Include(t => t.Regime)
                .Include(t => t.Estado)
                .FirstOrDefaultAsync(m => m.TratamentoId == id);
            if (tratamento == null)
            {
                return NotFound();
            }
            ViewData["GrauId"] = new SelectList(_context.Grau, "GrauId", "TipoGrau", tratamento.GrauId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nome", tratamento.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nome", tratamento.PacienteId);
            ViewData["PatologiaId"] = new SelectList(_context.Patologia, "PatologiaId", "Nome", tratamento.PatologiaId);
            ViewData["RegimeId"] = new SelectList(_context.Regime, "RegimeId", "TipoRegime", tratamento.RegimeId);
            ViewData["EstadoId"] = new SelectList(_context.Estado, "EstadoId", "Nome", tratamento.EstadoId);
            return View(tratamento);
        }

        // POST: Tratamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tratamento = await _context.Tratamentos.FindAsync(id);
            _context.Tratamentos.Remove(tratamento);
            await _context.SaveChangesAsync();
            TempData["deleteEnf"] = "Tratamento eliminado com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        private string EstadoTratamento(DateTime dataAtual,DateTime dataInicio)
        {
            string estado = null;

            int compare = DateTime.Compare(dataAtual, dataInicio);

            if (compare >= 0) //significa que a minha data atual é igual ou superior a data de inicio
            {
                estado = "A Decorrer";

            }
            else
            {
                estado = "Em Espera"; //significa que a minha data atual é inferior a data de inicio
            }


            return estado;
        }

        private bool DataTratamentoIsInvalid(DateTime inicioTratamento, DateTime fimTratamento) { 

            bool IsInvalid = false;

            DateTime dateNow = DateTime.Now;

            int x = DateTime.Compare(dateNow, inicioTratamento);
            int y = DateTime.Compare(dateNow, fimTratamento);
            int z = DateTime.Compare(inicioTratamento, fimTratamento);


            if (x >= 0 || y >= 0 || z <= 0) {

                IsInvalid = true;
            }
            return IsInvalid;

        }
        //falta adicionar para a data de fim 
        private bool TratamentoExists(int id)
        {
            return _context.Tratamentos.Any(e => e.TratamentoId == id);
        }
    }
}
