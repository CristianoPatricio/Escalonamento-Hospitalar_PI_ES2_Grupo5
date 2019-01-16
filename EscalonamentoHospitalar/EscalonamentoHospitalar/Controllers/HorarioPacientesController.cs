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
    public class HorarioPacientesController : Controller
    {
        private const int PAGE_SIZE = 20;
        private readonly HospitalDbContext _context;

        public HorarioPacientesController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: HorarioPacientes
        public async Task<IActionResult> Index(HorarioPacienteViewModel model = null, int page = 1)
        {

            string nome = null;
            DateTime? data = null;

            if (model != null && model.DataInicio != null || model.CurrentNome != null)
            {
                nome = model.CurrentNome;
                data = model.DataInicio;
                page = 1;
            }

            IQueryable<HorarioPaciente> horario;
            int numHorario;
            IEnumerable<HorarioPaciente> listaHorario;

            if (data.HasValue && string.IsNullOrEmpty(nome)) //Pesquisa por data
            {
                int ano = data.Value.Year;
                int mes = data.Value.Month;
                int dia = data.Value.Day;

                horario = _context.HorariosPaciente
                   .Where(h => h.DataInicio.Year.Equals(ano) && h.DataInicio.Month.Equals(mes) && h.DataInicio.Day.Equals(dia));

                numHorario = await horario.CountAsync();

                listaHorario = await horario
                    .Include(h => h.Paciente)
                    .OrderByDescending(h => h.DataInicio)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(nome) && !data.HasValue) //Pesquisa por Nome
            {
                horario = _context.HorariosPaciente
                    .Where(h => h.Paciente.Nome.Contains(nome.Trim()));

                numHorario = await horario.CountAsync();

                listaHorario = await horario
                    .Include(h => h.Paciente)
                    .OrderByDescending(h => h.DataInicio)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(nome) && data.HasValue) //Pesquisa por nome e data
            {
                int ano = data.Value.Year;
                int mes = data.Value.Month;
                int dia = data.Value.Day;

                horario = _context.HorariosPaciente
                    .Where(h => h.Paciente.Nome.Contains(nome.Trim()) && h.DataInicio.Year.Equals(ano) && h.DataInicio.Month.Equals(mes) && h.DataInicio.Day.Equals(dia));

                numHorario = await horario.CountAsync();

                listaHorario = await horario
                    .Include(h => h.Paciente)
                    .OrderByDescending(h => h.DataInicio)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else
            {
                horario = _context.HorariosPaciente;

                numHorario = await horario.CountAsync();

                listaHorario = await horario
                    .Include(h => h.Paciente)
                    .OrderByDescending(h => h.DataInicio)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }

            if (page > (numHorario / PAGE_SIZE) + 1)
            {
                page = 1;
            }

            if (listaHorario.Count() == 0)
            {
                TempData["NoItemsFound"] = "Não foram encontrados resultados para a sua pesquisa";
            }


            return View(
                new HorarioPacienteViewModel
                {
                    HorariosPacientes = listaHorario,
                    Pagination = new PagingViewModel
                    {
                        CurrentPage = page,
                        PageSize = PAGE_SIZE,
                        TotalItems = numHorario
                    },
                    CurrentNome = nome,
                    DataInicio = data
                }
            );

        }

        // GET: HorarioPacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioPaciente = await _context.HorariosPaciente
                .Include(h => h.Paciente)
                .FirstOrDefaultAsync(m => m.HorarioPacienteId == id);
            if (horarioPaciente == null)
            {
                return NotFound();
            }

            return View(horarioPaciente);
        }

        // GET: HorarioPacientes/Create
        public IActionResult Create()
        {
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "PacienteId");
            return View();
        }

        // POST: HorarioPacientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioPacienteId,DataInicio,Duracao,DataFim,PacienteId")] HorarioPaciente horarioPaciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horarioPaciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "CC", horarioPaciente.PacienteId);         
            return View(horarioPaciente);
        }

        // GET: HorarioPacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioPaciente = await _context.HorariosPaciente.FindAsync(id);
            if (horarioPaciente == null)
            {
                return NotFound();
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "CC", horarioPaciente.PacienteId);           
            return View(horarioPaciente);
        }

        // POST: HorarioPacientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioPacienteId,DataInicio,Duracao,DataFim,PacienteId")] HorarioPaciente horarioPaciente)
        {
            if (id != horarioPaciente.HorarioPacienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horarioPaciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioPacienteExists(horarioPaciente.HorarioPacienteId))
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
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "CC", horarioPaciente.PacienteId);         
            return View(horarioPaciente);
        }

        // GET: HorarioPacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioPaciente = await _context.HorariosPaciente
                .Include(h => h.Paciente)
                .FirstOrDefaultAsync(m => m.HorarioPacienteId == id);
            if (horarioPaciente == null)
            {
                return NotFound();
            }

            return View(horarioPaciente);
        }

        // POST: HorarioPacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horarioPaciente = await _context.HorariosPaciente.FindAsync(id);
            _context.HorariosPaciente.Remove(horarioPaciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioPacienteExists(int id)
        {
            return _context.HorariosPaciente.Any(e => e.HorarioPacienteId == id);
        }
    }
}
