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
      //  [Authorize(Roles = "Administrador")]
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
            ViewData["EstadoId"] = new SelectList(_context.Estado, "EstadoId", "Nome");

            return View();
        }

        // POST: Tratamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TratamentoId,PatologiaId,PacienteId,GrauId,DataInicio,DataFim,DuracaoCiclo,RegimeId,EstadoId,MedicoId")] Tratamento tratamento)
        {


            /******************Validações**********************/


            DateTime dateNow = DateTime.Now;
            DateTime inicioTratamento = tratamento.DataInicio;
            DateTime fimTratamento = tratamento.DataFim;


            //bool inicioTratamentoIsInvalid = true;

            





            //Validar Data de inicio do tratamento
            DateTime dataInicio = tratamento.DataInicio;
            DateTime dataFim = tratamento.DataFim;
            TimeSpan duracaoCiclo = tratamento.DuracaoCiclo;        
            Paciente idPaciente =  _context.Pacientes.SingleOrDefault(p => p.PacienteId == tratamento.PacienteId);
            int idRegime = tratamento.RegimeId;
            var tipoRegime = _context.Regime.SingleOrDefault(r => r.RegimeId == idRegime);
            string regime = tipoRegime.TipoRegime.ToString();

            Estado estado = _context.Estado.SingleOrDefault(e => e.Nome == "Decorrer");

            tratamento.EstadoId = estado.EstadoId;


            if (ModelState.IsValid)
            {
                _context.Add(tratamento);              
                await _context.SaveChangesAsync();

                TempData["notice"] = "Tratamento inserido com sucesso!";
                GenerateHorarioPaciente(_context, dataInicio, dataFim, duracaoCiclo,idPaciente, regime);

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

        private bool TratamentoExists(int id)
        {
            return _context.Tratamentos.Any(e => e.TratamentoId == id);
        }

        /*****************************Funções Auxiliares*********************************/
       
        /**
         * @param db
         * @param dataInicio
         * @param duracao
         * @param dataFim
         * @param pacienteId
         * @param tratamentoId
         * @insert into HorarioPaciente table a record with the above parameteres
         */ 
        private void InsertDataIntoHorarioPaciente(HospitalDbContext db, DateTime dataInicio, TimeSpan duracao, DateTime dataFim, Paciente pacienteId)
        {
            db.HorariosPaciente.Add(
                    new HorarioPaciente {DataInicio = dataInicio, Duracao = duracao, DataFim = dataFim, PacienteId = pacienteId.PacienteId}
                );

            db.SaveChanges();
        }

        /**
         * @param db 
         * @param dataInicio
         * @param dataFim
         * @param duracaoCiclo
         * @param idPaciente
         * @param regime
         * @generate a schedule for a specific client
         */
        private void GenerateHorarioPaciente(HospitalDbContext db, DateTime dataInicio, DateTime dataFim, TimeSpan duracaoCiclo,Paciente idPaciente, string regime)
         {
            List<DateTime> datasInicioTratamento = new List<DateTime>();
            List<DateTime> datasFimTratamento = new List<DateTime>();

            DateTime data;

            int iteracao = 0;
            if (regime == "Semanal")
            {
                iteracao = 7;
            }
            else if (regime == "Quinzenal")
            {
                iteracao = 14;
            }
            else if (regime == "Mensal")
            {
                iteracao = 30;
            }else if (regime == "Trimestral")
            {
                iteracao = 90;
            }

            double dif = dataFim.Subtract(dataInicio).TotalDays;

            double tempo = dif / iteracao;

            if (CheckIfThereIsMoreThanSevenClients(dataInicio.AddHours(9)) == true) { //Não há mais que sete
                data = dataInicio.AddHours(11);
            }
            else if (CheckIfThereIsMoreThanSevenClients(dataInicio.AddHours(11)) == true) //Não há mais que sete
            {
                data = dataInicio.AddHours(13);
            }else if (CheckIfThereIsMoreThanSevenClients(dataInicio.AddHours(13)) == true) //Não há mais que sete
            {
                data = dataInicio.AddHours(15);
            }
            else
            {
                data = dataInicio.AddHours(9);
            }

            for (int i = 0; i <= tempo; i++)
            {

                InsertDataIntoHorarioPaciente(db, data, duracaoCiclo, data.AddHours(duracaoCiclo.TotalHours), idPaciente);

                datasInicioTratamento.Add(data);
                datasFimTratamento.Add(data.AddHours(duracaoCiclo.TotalHours));

                data = data.AddDays(iteracao);             
            }
        }

        private bool CheckIfThereIsMoreThanSevenClients(DateTime data)
        {
            bool novaData = false;

            var horario = from h in _context.HorariosPaciente
                          where h.DataInicio.Day == data.Day && h.DataInicio.Month == data.Month && h.DataInicio.Year == data.Year && h.DataInicio.Hour == data.Hour
                          select h;

            if (horario.Count() >= 7)
            {
                novaData = true;
            }

            return novaData;
        }

    }
}
