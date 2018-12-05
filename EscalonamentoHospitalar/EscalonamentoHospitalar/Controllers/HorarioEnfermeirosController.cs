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
    public class HorarioEnfermeirosController : Controller
    {
        private const int PAGE_SIZE = 45;
        private readonly HospitalDbContext _context;

        public HorarioEnfermeirosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: HorarioEnfermeiros
        public async Task<IActionResult> Index(HorariosEnfermeirosViewModel model = null, int page = 1)
        {
            string nome = null;

            if (model != null && model.CurrentNome != null)
            {
                nome = model.CurrentNome;
                page = 1;
            }

            var horario = _context.HorariosEnfermeiro
                .Where(h => nome == null || h.Enfermeiro.Nome.Contains(nome)); ;

            int numHorario = await horario.CountAsync();

            if (page > (numHorario / PAGE_SIZE) + 1)
            {
                page = 1;
            }

            var listahorario = await horario
                .Include(h => h.Enfermeiro)
                .Include(h => h.Turno)
                .OrderBy(h => h.DataInicioTurno)
                .Skip(PAGE_SIZE * (page - 1))
                .Take(PAGE_SIZE)
                .ToListAsync();

            return View(
                new HorariosEnfermeirosViewModel
                {
                    HorariosEnfermeiros = listahorario,
                    Pagination = new PagingViewModel
                    {
                        CurrentPage = page,
                        PageSize = PAGE_SIZE,
                        TotalItems = numHorario
                    },
                    CurrentNome = nome
                }
            );
        }

        // GET: HorarioEnfermeiros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioEnfermeiro = await _context.HorariosEnfermeiro
                .Include(h => h.Enfermeiro)
                .Include(h => h.Turno)
                .FirstOrDefaultAsync(m => m.HorarioEnfermeiroId == id);
            if (horarioEnfermeiro == null)
            {
                return NotFound();
            }

            return View(horarioEnfermeiro);
        }

        // GET: HorarioEnfermeiros/Create
        public IActionResult Create()
        {
            ViewData["EnfermeiroId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "CC");
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId");
            return View();
        }

        // POST: HorarioEnfermeiros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioEnfermeiroId,DataInicioTurno,Duracao,DataFimTurno,TurnoId,EnfermeiroId")] HorarioEnfermeiro horarioEnfermeiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horarioEnfermeiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnfermeiroId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "CC", horarioEnfermeiro.EnfermeiroId);
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId", horarioEnfermeiro.TurnoId);
            return View(horarioEnfermeiro);
        }

        // GET: HorarioEnfermeiros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioEnfermeiro = await _context.HorariosEnfermeiro.FindAsync(id);
            if (horarioEnfermeiro == null)
            {
                return NotFound();
            }
            ViewData["EnfermeiroId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "CC", horarioEnfermeiro.EnfermeiroId);
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId", horarioEnfermeiro.TurnoId);
            return View(horarioEnfermeiro);
        }

        // POST: HorarioEnfermeiros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioEnfermeiroId,DataInicioTurno,Duracao,DataFimTurno,TurnoId,EnfermeiroId")] HorarioEnfermeiro horarioEnfermeiro)
        {
            if (id != horarioEnfermeiro.HorarioEnfermeiroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horarioEnfermeiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioEnfermeiroExists(horarioEnfermeiro.HorarioEnfermeiroId))
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
            ViewData["EnfermeiroId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "CC", horarioEnfermeiro.EnfermeiroId);
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId", horarioEnfermeiro.TurnoId);
            return View(horarioEnfermeiro);
        }

        // GET: HorarioEnfermeiros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioEnfermeiro = await _context.HorariosEnfermeiro
                .Include(h => h.Enfermeiro)
                .Include(h => h.Turno)
                .FirstOrDefaultAsync(m => m.HorarioEnfermeiroId == id);
            if (horarioEnfermeiro == null)
            {
                return NotFound();
            }

            return View(horarioEnfermeiro);
        }

        // POST: HorarioEnfermeiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horarioEnfermeiro = await _context.HorariosEnfermeiro.FindAsync(id);
            _context.HorariosEnfermeiro.Remove(horarioEnfermeiro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioEnfermeiroExists(int id)
        {
            return _context.HorariosEnfermeiro.Any(e => e.HorarioEnfermeiroId == id);
        }

        // GET: HorarioEnfermeiro/GerarHorarioEnfermeiro
        public IActionResult GerarHorarioEnfermeiro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GerarHorarioEnfermeiro(GerarHorarioEnfermeiro gerarHorarioEnfermeiro)
        {
            //Variáveis
            int numPessoasT1 = gerarHorarioEnfermeiro.NumeroPessoasTurno1;
            int numPessoasT2 = gerarHorarioEnfermeiro.NumeroPessoasTurno2;
            int numPessoasT3 = gerarHorarioEnfermeiro.NumeroPessoasTurno3;

            DateTime dataInicio = gerarHorarioEnfermeiro.DataInicioSemana;

            int ano = dataInicio.Year;
            int mes = dataInicio.Month;
            int dia = dataInicio.Day;

            if (ModelState.IsValid)
            {
                //Função que insere registo na BD
                GenerateHorarioEnfermeiro(_context, numPessoasT1, numPessoasT2, numPessoasT3, ano, mes, dia);
            }
            return RedirectToAction(nameof(Index));
        }

        /*************************Funções Auxiliares************************/
        private void GenerateHorarioEnfermeiro(HospitalDbContext db, int numT1, int numT2, int numT3, int ano, int mes, int dia)
        {
            //Variáveis
            int numPessoasT1 = numT1;
            int numPessoasT2 = numT2;
            int numPessoasT3 = numT3;

            int segunda = 2;
            int sexta = 6;

            int[] enfermeiros = EnfermeirosIds();
            int[] enfermeirosSemFilhos = EnfermeirosSemFilhosIds();
            int[] enfermeirosComFilhos = EnfermeirosComFilhosIds();

            int enfT1 = 0;
            int enfT2 = 0;
            int enfT3 = 0;

            int nEnfComFolgaPorDia = enfermeiros.Length - (numPessoasT1 + numPessoasT2 + numPessoasT3);

            int[] idEnfComFolga = new int[nEnfComFolgaPorDia];

            int[] idEnfNoite = null;

            //Lista enfermeiros
            List<int> listaEnfermeiros = new List<int>(enfermeiros);

            //Lista de enfermeiros sem filhos
            List<int> listaEnfermeirosSemFilhos;

            //Lista de enfermeiros com filhos
            List<int> listaEnfermeirosComFilhos;

            Random rnd = new Random();

            DateTime data;

            for (int i = segunda; i <= sexta; i++) //para cada dia da semana
            {
                listaEnfermeirosSemFilhos = new List<int>(enfermeirosSemFilhos);
                listaEnfermeirosComFilhos = new List<int>(enfermeirosComFilhos);

                for (int m = 0; m < nEnfComFolgaPorDia; m++) //para cada nEnf de folga por dia
                {
                    //escolhe aleatoriamente um enfermeiro para folga;
                    idEnfComFolga[m] = listaEnfermeiros[rnd.Next(0, listaEnfermeiros.Count())];

                    //remover enf da lista
                    listaEnfermeiros.Remove(idEnfComFolga[m]);
                    //remove enf da lista
                    listaEnfermeirosComFilhos.Remove(idEnfComFolga[m]);
                    //remove enf da lista
                    listaEnfermeirosSemFilhos.Remove(idEnfComFolga[m]);

                    //Console.Write("Folgas: " + idEnfComFolga[m] + "\n");
                }

                //remover da lista enfermeiros que fizeram noite
                if (idEnfNoite != null)
                {
                    for (int n = 0; n < numPessoasT3; n++)
                    {
                        if (listaEnfermeirosSemFilhos.Contains(idEnfNoite[n]))
                        {
                            listaEnfermeirosSemFilhos.Remove(idEnfNoite[n]);
                        }
                    }
                }

                for (int j = 0; j < numPessoasT1; j++) //para cada nPessoas do Turno 1
                {
                    string turno = "MANHÃ";

                    int duracao = 8;

                    Turno turnoId = _context.Turnos.SingleOrDefault(t => t.Nome.Equals(turno));

                    if (j % 2 == 0 && listaEnfermeirosComFilhos.Count() != 0) //escolhe enfermeiros com filhos
                    {
                        //escolhe aleatoriamente um enfermeiro para o turno
                        enfT1 = listaEnfermeirosComFilhos[rnd.Next(0, listaEnfermeirosComFilhos.Count())];
                        data = new DateTime(ano, mes, dia, 9, 0, 0);
                    }
                    else //escolhe enfermeiros sem filhos
                    {
                        //escolhe aleatoriamente um enfermeiro para o turno
                        enfT1 = listaEnfermeirosSemFilhos[rnd.Next(0, listaEnfermeirosSemFilhos.Count())];           
                        data = new DateTime(ano, mes, dia, 8, 0, 0);
                    }

                    Enfermeiro enfermeiroIdT1 = _context.Enfermeiros.SingleOrDefault(e => e.EnfermeiroId == enfT1);

                    //Adicionar função para inserir na BD
                    InsertDataIntoHorarioEnfermeiro(db, data.AddDays(i - 2), duracao, data.AddDays(i - 2).AddHours(duracao), turnoId, enfermeiroIdT1);

                    //remove da lista o enfermeiro do turno
                    listaEnfermeirosComFilhos.Remove(enfT1);
                    //remove da lista o enfermeiro do turno
                    listaEnfermeirosSemFilhos.Remove(enfT1);
                }

                //adicionar enfermeiros que fizeram noite novamente à lista
                if (idEnfNoite != null)
                {
                    for (int n = 0; n < numPessoasT3; n++)
                    {
                        if (!listaEnfermeirosSemFilhos.Contains(idEnfNoite[n]))
                        {
                            listaEnfermeirosSemFilhos.Add(idEnfNoite[n]);
                        }
                    }
                    for (int u = 0; u < nEnfComFolgaPorDia; u++)
                    {
                        listaEnfermeirosSemFilhos.Remove(idEnfComFolga[u]);
                    }

                }         

                if (i != sexta)
                {
                    for (int k = 0; k < numPessoasT2; k++) // para cada nPesosas do Turno 2
                    {
                        string turno = "TARDE";

                        Turno turnoId = _context.Turnos.SingleOrDefault(t => t.Nome.Equals(turno));

                        int duracao = 8;

                        if (k % 2 == 0 && listaEnfermeirosComFilhos.Count() != 0)
                        {
                            enfT2 = listaEnfermeirosComFilhos[rnd.Next(0, listaEnfermeirosComFilhos.Count())];
                        }
                        else
                        {
                            enfT2 = listaEnfermeirosSemFilhos[rnd.Next(0, listaEnfermeirosSemFilhos.Count())];
                        }

                        data = new DateTime(ano, mes, dia, 16, 0, 0);

                        Enfermeiro enfermeiroIdT2 = _context.Enfermeiros.SingleOrDefault(e => e.EnfermeiroId == enfT2);

                        //Adicionar função para inserir na BD
                        InsertDataIntoHorarioEnfermeiro(db, data.AddDays(i - 2), duracao, data.AddDays(i - 2).AddHours(duracao), turnoId, enfermeiroIdT2);

                        //remove da lista o enfermeiro do turno
                        listaEnfermeirosComFilhos.Remove(enfT2);
                        //remove da lista o enfermeiro do turno
                        listaEnfermeirosSemFilhos.Remove(enfT2);
                    }
                }
 
                //Reinicialização do array idEnfNoite
                idEnfNoite = new int[numPessoasT3];

                if (i != sexta)
                {
                    for (int l = 0; l < numPessoasT3; l++) // para cada nPessoas do Turno 3
                    {
                        string turno = "NOITE";

                        Turno turnoId = _context.Turnos.SingleOrDefault(t => t.Nome.Equals(turno));

                        int duracao = 8;

                        enfT3 = listaEnfermeirosSemFilhos[rnd.Next(0, listaEnfermeirosSemFilhos.Count())];
                        idEnfNoite[l] = enfT3;

                        data = new DateTime(ano, mes, dia + 1, 0, 0, 0);

                        Enfermeiro enfermeiroIdT3 = _context.Enfermeiros.SingleOrDefault(e => e.EnfermeiroId == enfT3);

                        //Adicionar função para inserir na BD
                        InsertDataIntoHorarioEnfermeiro(db, data.AddDays(i - 2), duracao, data.AddDays(i - 2).AddHours(duracao), turnoId, enfermeiroIdT3);                 

                        //remove da lista o enfermeiro do turno
                        listaEnfermeirosSemFilhos.Remove(enfT3);
                    }
                }
            }      
        }

        /**
        * @return an array with nurse's id's  
        */
        private int[] EnfermeirosIds()
        {
            var enfermeiros = from e in _context.Enfermeiros
                              select e.EnfermeiroId;

            int[] arrIdEnfermeiros = enfermeiros.ToArray();

            return arrIdEnfermeiros;
        }

        /**
        * @return an array with nurse's id's who have son's
        */
        private int[] EnfermeirosComFilhosIds()
        {
            var enfermeirosComFilhos = from e in _context.Enfermeiros
                                       where e.Filhos == true
                                       select e.EnfermeiroId;

            int[] arrIdEnfermeirosComFilhos = enfermeirosComFilhos.ToArray();

            return arrIdEnfermeirosComFilhos;
        }

        /**
        * @return an array with nurse's id's who haven't son's
        */
        private int[] EnfermeirosSemFilhosIds()
        {
            var enfermeirosSemFilhos = from e in _context.Enfermeiros
                                       where e.Filhos == false
                                       select e.EnfermeiroId;

            int[] arrIdEnfermeirosSemFilhos = enfermeirosSemFilhos.ToArray();

            return arrIdEnfermeirosSemFilhos;
        }


        private void InsertDataIntoHorarioEnfermeiro(HospitalDbContext db, DateTime dataInicioTurno, int duracao, DateTime dataFimTurno, Turno turnoId, Enfermeiro enfermeiroId)
        {
            db.HorariosEnfermeiro.Add(
                new HorarioEnfermeiro { DataInicioTurno = dataInicioTurno, Duracao = duracao, DataFimTurno = dataInicioTurno.AddHours(duracao), TurnoId = turnoId.TurnoId, EnfermeiroId = enfermeiroId.EnfermeiroId }
            );

            db.SaveChanges();
        }

    }
}
