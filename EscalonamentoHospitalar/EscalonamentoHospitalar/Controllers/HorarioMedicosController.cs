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
    public class HorarioMedicosController : Controller
    {
        private const int PAGE_SIZE = 10;
        private readonly HospitalDbContext _context;

        public HorarioMedicosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: HorarioMedicos
        public async Task<IActionResult> Index(HorariosMedicosViewModel model = null, int page = 1)
        {
            string nome = null;
            DateTime? data = null;

            if (model != null && model.DataInicio != null || model.CurrentNome != null)
            {
                nome = model.CurrentNome;
                data = model.DataInicio;
                page = 1;
            }

            IQueryable<HorarioMedico> horario;
            int numHorario;
            IEnumerable<HorarioMedico> listaHorario;

            if (data.HasValue && string.IsNullOrEmpty(nome)) //Pesquisa por data
            {
                int ano = data.Value.Year;
                int mes = data.Value.Month;
                int dia = data.Value.Day;

                horario = _context.HorariosMedicos
                   .Where(h => h.DataInicioTurno.Year.Equals(ano) && h.DataInicioTurno.Month.Equals(mes) && h.DataInicioTurno.Day.Equals(dia));

                numHorario = await horario.CountAsync();

                listaHorario = await horario
                    .Include(h => h.Medico)
                    .Include(h => h.Turno)
                    .OrderBy(h => h.DataInicioTurno)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(nome) && !data.HasValue) //Pesquisa por Nome
            {
                horario = _context.HorariosMedicos
                    .Where(h => h.Medico.Nome.Contains(nome.Trim()));

                numHorario = await horario.CountAsync();

                listaHorario = await horario
                    .Include(h => h.Medico)
                    .Include(h => h.Turno)
                    .OrderBy(h => h.DataInicioTurno)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(nome) && data.HasValue) //Pesquisa por nome e data
            {
                int ano = data.Value.Year;
                int mes = data.Value.Month;
                int dia = data.Value.Day;

                horario = _context.HorariosMedicos
                    .Where(h => h.Medico.Nome.Contains(nome.Trim()) && h.DataInicioTurno.Year.Equals(ano) && h.DataInicioTurno.Month.Equals(mes) && h.DataInicioTurno.Day.Equals(dia));

                numHorario = await horario.CountAsync();

                listaHorario = await horario
                  .Include(h => h.Medico)
                  .Include(h => h.Turno)
                  .OrderBy(h => h.DataInicioTurno)
                  .Skip(PAGE_SIZE * (page - 1))
                  .Take(PAGE_SIZE)
                  .ToListAsync();
            }
            else
            {
                horario = _context.HorariosMedicos;

                numHorario = await horario.CountAsync();

                listaHorario = await horario
                  .Include(h => h.Medico)
                  .Include(h => h.Turno)
                  .OrderBy(h => h.DataInicioTurno)
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
                new HorariosMedicosViewModel
                {
                    HorariosMedicos = listaHorario,
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

        // GET: HorarioMedicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioMedico = await _context.HorariosMedicos
                .Include(h => h.Medico)
                .Include(h => h.Turno)
                .FirstOrDefaultAsync(m => m.HorarioMedicoId == id);
            if (horarioMedico == null)
            {
                return NotFound();
            }

            return View(horarioMedico);
        }

        // GET: HorarioMedicos/Create
        public IActionResult Create()
        {
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "CC");
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId");
            return View();
        }

        // POST: HorarioMedicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioMedicoId,DataInicioTurno,Duracao,DataFimTurno,TurnoId,MedicoId")] HorarioMedico horarioMedico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horarioMedico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "CC", horarioMedico.MedicoId);
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId", horarioMedico.TurnoId);
            return View(horarioMedico);
        }

        // GET: HorarioMedicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioMedico = await _context.HorariosMedicos.FindAsync(id);
            if (horarioMedico == null)
            {
                return NotFound();
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "CC", horarioMedico.MedicoId);
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId", horarioMedico.TurnoId);
            return View(horarioMedico);
        }

        // POST: HorarioMedicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioMedicoId,DataInicioTurno,Duracao,DataFimTurno,TurnoId,MedicoId")] HorarioMedico horarioMedico)
        {
            if (id != horarioMedico.HorarioMedicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horarioMedico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioMedicoExists(horarioMedico.HorarioMedicoId))
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
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "CC", horarioMedico.MedicoId);
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId", horarioMedico.TurnoId);
            return View(horarioMedico);
        }

        // GET: HorarioMedicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioMedico = await _context.HorariosMedicos
                .Include(h => h.Medico)
                .Include(h => h.Turno)
                .FirstOrDefaultAsync(m => m.HorarioMedicoId == id);
            if (horarioMedico == null)
            {
                return NotFound();
            }

            return View(horarioMedico);
        }

        // POST: HorarioMedicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horarioMedico = await _context.HorariosMedicos.FindAsync(id);
            _context.HorariosMedicos.Remove(horarioMedico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioMedicoExists(int id)
        {
            return _context.HorariosMedicos.Any(e => e.HorarioMedicoId == id);
        }

        // GET: HorarioEnfermeiro/GerarHorarioEnfermeiro
        public IActionResult GerarHorarioMedico()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GerarHorarioMedico([Bind("NumeroPessoasTurno1, NumeroPessoasTurno2, DataInicioSemana")] GerarHorarioMedico gerarHorarioMedico)
        {
            //Variáveis
            int numPessoasT1 = gerarHorarioMedico.NumeroPessoasTurno1;
            int numPessoasT2 = gerarHorarioMedico.NumeroPessoasTurno2;

            DateTime dataInicio = gerarHorarioMedico.DataInicioSemana;

            int ano = dataInicio.Year;
            int mes = dataInicio.Month;
            int dia = dataInicio.Day;

            /**********Validações***********/

            //Validar se Data de Início de Semana é uma segunda-feira
            if (DataInicioSemanaIsNotAMonday(dataInicio) == true)
            {
                //Mensagem de erro caso os médicos por turno não sejam suficientes para gerar o horário
                ModelState.AddModelError("DataInicioSemana", "Tem de selecionar uma data correspondente a uma segunda-feira e/ou igual ou superior à data atual");
            }

            //Validar Numero de Médicos por Turno
            if (NumMedicosPorTurnoIsInvalid(numPessoasT1, numPessoasT2) == true)
            {
                //Mensagem de erro caso os médicos por turno não sejam suficientes para gerar o horário
                ModelState.AddModelError("NumeroPessoasTurno3", "Não tem médicos suficientes para todos os turnos. Por favor, verifique os campos e tente novamente");
            }

            if (ModelState.IsValid)
            {
                if (!DataInicioSemanaIsNotAMonday(dataInicio) || !NumMedicosPorTurnoIsInvalid(numPessoasT1, numPessoasT2))
                {
                    //Função que insere registo na BD
                    GenerateHorarioMedico(_context, numPessoasT1, numPessoasT2, ano, mes, dia);
                    TempData["Success"] = "Horário gerado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(gerarHorarioMedico);
        }

        // GET: HorarioMedico/PedidoTrocaTurnoMedico
        public async Task<IActionResult> PedidoTrocaTurnoMedico(int? idHorario1, HorariosMedicosViewModel model = null, int page = 1)
        {
            ViewBag.HorarioATrocar = idHorario1;

            if (idHorario1 == null)
            {
                return NotFound();
            }

            //Select MedicoID Where HorarioMedicoId = idHorario1
            var idMed = from h in _context.HorariosMedicos
                        where h.HorarioMedicoId == idHorario1
                        select h.MedicoId;

            var dataInicio = from h in _context.HorariosMedicos
                             where h.HorarioMedicoId == idHorario1
                             select h.DataInicioTurno;

            string nome = null;
            DateTime? data = null;

            if (model != null && model.DataInicio != null || model.CurrentNome != null)
            {
                nome = model.CurrentNome;
                data = model.DataInicio;
                page = 1;
            }

            IQueryable<HorarioMedico> horario;
            int numHorario;
            IEnumerable<HorarioMedico> listaHorario;

            if (data.HasValue && string.IsNullOrEmpty(nome)) //Pesquisa por data
            {
                int ano = data.Value.Year;
                int mes = data.Value.Month;
                int dia = data.Value.Day;

                horario = _context.HorariosMedicos
                   .Where(h => h.DataInicioTurno.Year.Equals(ano) && h.DataInicioTurno.Month.Equals(mes) && h.DataInicioTurno.Day.Equals(dia) && h.HorarioMedicoId != idHorario1 && h.MedicoId != idMed.Single() && h.DataInicioTurno >= dataInicio.Single());

                numHorario = await horario.CountAsync();

                listaHorario = await horario
                    .Include(h => h.Medico)
                    .Include(h => h.Turno)
                    .OrderBy(h => h.DataInicioTurno)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(nome) && !data.HasValue) //Pesquisa por Nome
            {
                horario = _context.HorariosMedicos
                    .Where(h => h.Medico.Nome.Contains(nome.Trim()) && h.HorarioMedicoId != idHorario1 && h.MedicoId != idMed.Single() && h.DataInicioTurno >= dataInicio.Single());

                numHorario = await horario.CountAsync();

                listaHorario = await horario
                    .Include(h => h.Medico)
                    .Include(h => h.Turno)
                    .OrderBy(h => h.DataInicioTurno)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(nome) && data.HasValue) //Pesquisa por nome e data
            {
                int ano = data.Value.Year;
                int mes = data.Value.Month;
                int dia = data.Value.Day;

                horario = _context.HorariosMedicos
                    .Where(h => h.Medico.Nome.Contains(nome.Trim()) && h.DataInicioTurno.Year.Equals(ano) && h.DataInicioTurno.Month.Equals(mes) && h.DataInicioTurno.Day.Equals(dia) && h.HorarioMedicoId != idHorario1 && h.MedicoId != idMed.Single() && h.DataInicioTurno >= dataInicio.Single());

                numHorario = await horario.CountAsync();

                listaHorario = await horario
                  .Include(h => h.Medico)
                  .Include(h => h.Turno)
                  .OrderBy(h => h.DataInicioTurno)
                  .Skip(PAGE_SIZE * (page - 1))
                  .Take(PAGE_SIZE)
                  .ToListAsync();
            }
            else
            {
                horario = _context.HorariosMedicos
                     .Where(h => h.HorarioMedicoId != idHorario1 && h.MedicoId != idMed.Single() && h.DataInicioTurno >= dataInicio.Single());

                numHorario = await horario.CountAsync();

                listaHorario = await horario
                  .Include(h => h.Medico)
                  .Include(h => h.Turno)
                  .OrderBy(h => h.DataInicioTurno)
                  .Skip(PAGE_SIZE * (page - 1))
                  .Take(PAGE_SIZE)
                  .ToListAsync();
            }

            if (page > (numHorario / PAGE_SIZE) + 1)
            {
                page = 1;
            }

            return View(
                new HorariosMedicosViewModel
                {
                    HorariosMedicos = listaHorario,
                    Pagination = new PagingViewModel
                    {
                        CurrentPage = page,
                        PageSize = PAGE_SIZE,
                        TotalItems = numHorario
                    },
                    CurrentNome = nome,
                    DataInicio = data
                });
        }

        //GET: HorarioMedico/SolicitarPedidoTrocaTurnoMedico
        public async Task<IActionResult> SolicitarPedidoTrocaTurnoMedico(int? idHorario1, int? idHorario2)
        {
            if (idHorario1 == null || idHorario2 == null)
            {
                return NotFound();
            }

            var horarioMedico1 = await _context.HorariosMedicos
                .Include(h => h.Medico)
                .Include(h => h.Turno)
                .FirstOrDefaultAsync(m => m.HorarioMedicoId == idHorario1);

            var horarioMedico2 = await _context.HorariosMedicos
                .Include(h => h.Medico)
                .Include(h => h.Turno)
                .FirstOrDefaultAsync(m => m.HorarioMedicoId == idHorario2);

            if (horarioMedico1 == null || horarioMedico2 == null)
            {
                return NotFound();
            }

            ViewBag.HorarioATrocar = idHorario1;
            ViewBag.HorarioParaTroca = idHorario2;

            return View(

                new PedidoTrocaTurnosMedicoViewModel
                {
                    horarioMedicoATrocar = horarioMedico1,
                    horarioMedicoParaTroca = horarioMedico2
                }

                );
        }

        //POST:HorarioEnfermeiro/SolicitarPedidoTrocaTurnoMedico
        [HttpPost, ActionName("SolicitarPedidoTrocaTurnoMedico")]
        [ValidateAntiForgeryToken]
        public IActionResult SolicitarPedidoTrocaTurnoMedicoConfirmed(int idHor1, int idHor2)
        {
            DateTime dataPedido = DateTime.Now;

            // Verifica se já existe um pedido feito com os id's dos horários
            if (PedidoTrocaTurnoJaFoiEfetudado(idHor1, idHor2) == true)
            {
                TempData["PedidoAlreadyDone"] = "Já existe um pedido feito para a troca destes horários";
                return RedirectToAction(nameof(Index));
            }

            //Select MedicoID Where HorarioMedicoId = idHorario1
            var idMedRequerente = from h in _context.HorariosMedicos
                                  where h.HorarioMedicoId == idHor1
                                  select h.MedicoId;

            HorarioMedico horarioATrocar = _context.HorariosMedicos.SingleOrDefault(h => h.HorarioMedicoId == idHor1);
            HorarioMedico horarioParaTroca = _context.HorariosMedicos.SingleOrDefault(h => h.HorarioMedicoId == idHor2);

            try
            {
                //Insert into HorarioATrocarMedico
                InsertDataIntoHorarioATrocarMedico(_context, horarioATrocar);

                //Insert into HorarioParaTrocaMedico
                InsertDataIntoHorarioParaTrocaMedico(_context, horarioParaTroca);
            }
            catch (DbUpdateConcurrencyException)
            {
                TempData["ErrorRequired"] = "Erro ao inserir pedido!";
                return RedirectToAction(nameof(Index));
            }

            HorarioATrocarMedico horarioATrocarId = _context.HorarioATrocarMedico.LastOrDefault(h => h.HorarioMedicoId == idHor1);
            HorarioParaTrocaMedico horarioParaTrocaId = _context.HorarioParaTrocaMedico.LastOrDefault(h => h.HorarioMedicoId == idHor2);

            Medico medicoRequerenteId = _context.Medicos.SingleOrDefault(e => e.MedicoId == idMedRequerente.Single());

            EstadoPedidoTroca estadoPedidoTrocaId = _context.EstadoPedidoTrocas.SingleOrDefault(e => e.Nome == "Pendente");

            //Insert into PedidoTrocaTurnos Table
            try
            {
                if (!PedidoTrocaTurnoJaFoiEfetudado(idHor1, idHor2))
                {
                    InsertDataIntoPedidoTrocaTurnoMedico(_context, dataPedido, medicoRequerenteId, horarioATrocarId, horarioParaTrocaId, estadoPedidoTrocaId);
                    TempData["SuccessRequired"] = "Pedido realizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                TempData["ErrorRequired"] = "Erro ao inserir pedido!";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        /************************Funções Auxiliares*************************/

        /**
        * @param db
        * @param numT1
        * @param numT2
        * @param ano
        * @param mes
        * @param dia
        * @generate a random shcedule for the doctors
        */
        private void GenerateHorarioMedico(HospitalDbContext db, int numT1, int numT2, int ano, int mes, int dia)
        {
            //Variáveis
            int numPessoasT1 = 1;
            int numPessoasT2 = 1;

            int segunda = 2;
            int sexta = 6;

            int[] medicos = MedicosIds();

            int medT1 = 0;
            int medT2 = 0;

            //Lista médicos
            List<int> listaMedicos;

            Random rnd = new Random();

            DateTime data;

            for (int i = segunda; i <= sexta; i++) //para cada dia da semana
            {
                listaMedicos = new List<int>(medicos);

                for (int j = 0; j < numPessoasT1; j++) //para cada nPessoas do Turno 1
                {
                    string turno = "MANHÃ-2";
                    int duracao = 8;

                    //escolhe aleatoriamente um médicos para o turno
                    medT1 = listaMedicos[rnd.Next(0, listaMedicos.Count())];
                    data = new DateTime(ano, mes, dia, 9, 0, 0);

                    Turno turnoId = _context.Turnos.SingleOrDefault(t => t.Nome.Equals(turno));
                    Medico medicoIdT1 = _context.Medicos.SingleOrDefault(m => m.MedicoId == medT1);

                    //Adicionar função para inserir na BD
                    InsertDataIntoHorarioMedico(db, data.AddDays(i - 2), duracao, data.AddDays(i - 2).AddHours(duracao), turnoId, medicoIdT1);

                    //remove da lista o médicos do turno
                    listaMedicos.Remove(medT1);
                }
               
                for (int k = 0; k < numPessoasT2; k++) // para cada nPesosas do Turno 2
                {
                    string turno = "TARDE";
                    int duracao = 8;

                    medT2 = listaMedicos[rnd.Next(0, listaMedicos.Count())];
                    data = new DateTime(ano, mes, dia, 16, 0, 0);

                    Turno turnoId = _context.Turnos.SingleOrDefault(t => t.Nome.Equals(turno));
                    Medico medicoIdT2 = _context.Medicos.SingleOrDefault(m => m.MedicoId == medT2);

                    //Adicionar função para inserir na BD
                    InsertDataIntoHorarioMedico(db, data.AddDays(i - 2), duracao, data.AddDays(i - 2).AddHours(duracao), turnoId, medicoIdT2);

                    //remove da lista o médico do turno
                    listaMedicos.Remove(medT2);
                }

            }
        }

        /**
        * @param db
        * @param dataInicioTurno
        * @param duracao
        * @param dataFimTurno
        * @param turnoId
        * @param medicoId
        * @insert in the HorarioMedico table a record with the above parameters
        */
        private void InsertDataIntoHorarioMedico(HospitalDbContext db, DateTime dataInicioTurno, int duracao, DateTime dataFimTurno, Turno turnoId, Medico medicoId)
        {
            db.HorariosMedicos.Add(
                new HorarioMedico { DataInicioTurno = dataInicioTurno, Duracao = duracao, DataFimTurno = dataInicioTurno.AddHours(duracao), TurnoId = turnoId.TurnoId, MedicoId = medicoId.MedicoId }
            );

            db.SaveChanges();
        }

        /**
        * @param data
        * @return true if the day of the selected date isn't a Monday
        */
        private bool DataInicioSemanaIsNotAMonday(DateTime data)
        {
            bool IsInvalid = false;
            DateTime dateNow = DateTime.Now;

            int dateTimeCompare = DateTime.Compare(data, dateNow);

            if ((data.DayOfWeek != DayOfWeek.Monday) || dateTimeCompare < 0)
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        /**
        * @return an array with doctor's id's  
        */
        private int[] MedicosIds()
        {
            var medicos = from m in _context.Medicos
                              select m.MedicoId;

            int[] arrIdMedicos = medicos.ToArray();

            return arrIdMedicos;
        }

        /**
        * @param numT1
        * @param numT2
        * @return true if total doctors is less than sum of numT1 + numT2
        */
        private bool NumMedicosPorTurnoIsInvalid(int numT1, int numT2)
        {
            bool IsInvalid = false;

            int totalMed = numT1 + numT2;

            if (MedicosIds().Length <= totalMed)
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        /**
       * @param idHorarioATrocar
       * @param idHorarioParaTroca
       * @return true if a request with above parameters was already done.
       */
        private bool PedidoTrocaTurnoJaFoiEfetudado(int idHorarioATrocar, int idHorarioParaTroca)
        {
            bool pedidoEfetuado = false;

            var pedido = from p in _context.PedidoTrocaTurnosMedico
                         where p.HorarioATrocarMedico.HorarioMedicoId == idHorarioATrocar || p.HorarioParaTrocaMedico.HorarioMedicoId == idHorarioParaTroca
                         select p;

            if (pedido.Count() != 0)
            {
                pedidoEfetuado = true;
            }

            return pedidoEfetuado;
        }

        /**
        * @param db
        * @param horarioATrocar
        * @insert in the HorarioATrocarMedico table a record with the above parameters
        */
        private void InsertDataIntoHorarioATrocarMedico(HospitalDbContext db, HorarioMedico horarioATrocar)
        {
            db.HorarioATrocarMedico.Add(

                new HorarioATrocarMedico { HorarioMedicoId = horarioATrocar.HorarioMedicoId }

                );

            db.SaveChanges();
        }

        /**
        * @param db
        * @param horarioParaTroca
        * @insert in the HorarioParaTrocaMedico table a record with the above parameters
        */
        private void InsertDataIntoHorarioParaTrocaMedico(HospitalDbContext db, HorarioMedico horarioParaTroca)
        {
            db.HorarioParaTrocaMedico.Add(

                new HorarioParaTrocaMedico { HorarioMedicoId = horarioParaTroca.HorarioMedicoId }

                );

            db.SaveChanges();
        }

        /**
        * @param db
        * @param dataPedido
        * @param medicoRequerente
        * @param horarioATrocarId
        * @param horarioParaTrocaId
        * @param estadoPedidoTrocaId
        * @insert in the PedidoTrocaTurnoMedico table a record with the above parameters
        */
        private void InsertDataIntoPedidoTrocaTurnoMedico(HospitalDbContext db, DateTime dataPedido, Medico medicoRequerenteId, HorarioATrocarMedico horarioATrocarId, HorarioParaTrocaMedico horarioParaTrocaId, EstadoPedidoTroca estadoPedidoTrocaId)
        {
            db.PedidoTrocaTurnosMedico.Add(

                new PedidoTrocaTurnosMedico { DataPedido = dataPedido, MedicoId = medicoRequerenteId.MedicoId, HorarioATrocarMedicoId = horarioATrocarId.HorarioATrocarMedicoId, HorarioParaTrocaMedicoId = horarioParaTrocaId.HorarioParaTrocaMedicoId, EstadoPedidoTrocaId = estadoPedidoTrocaId.EstadoPedidoTrocaId }

               );

            db.SaveChanges();
        }


    }
}
