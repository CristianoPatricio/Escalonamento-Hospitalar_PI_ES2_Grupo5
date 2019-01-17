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
    public class PedidoTrocaTurnosEnfermeirosController : Controller
    {
        private const int PAGE_SIZE = 10;
        private readonly HospitalDbContext _context;

        public PedidoTrocaTurnosEnfermeirosController(HospitalDbContext context)
        {
            _context = context;
        }

        public IActionResult Error()
        {
            return View();
        }

        // GET: PedidoTrocaTurnosEnfermeiros
        public async Task<IActionResult> Index(ListaPedidoTrocaTurnosEnfermeiroViewModel model = null, int page = 1)
        {
            string nome = null;
            DateTime? data = null;

            if (model != null && model.DataInicio != null || model.CurrentNome != null)
            {
                nome = model.CurrentNome;
                data = model.DataInicio;
                page = 1;
            }

            IQueryable<PedidoTrocaTurnosEnfermeiro> pedidoTrocaTurnosEnfermeiro;
            int numHorario;
            IEnumerable<PedidoTrocaTurnosEnfermeiro> listaPedidosTrocaTurnoEnfermeiros;

            if (data.HasValue && string.IsNullOrEmpty(nome)) //Pesquisa por data
            {
                int ano = data.Value.Year;
                int mes = data.Value.Month;
                int dia = data.Value.Day;

                pedidoTrocaTurnosEnfermeiro = _context.PedidoTrocaTurnosEnfermeiros
                   .Where(h => h.DataPedido.Year.Equals(ano) && h.DataPedido.Month.Equals(mes) && h.DataPedido.Day.Equals(dia));

                numHorario = await pedidoTrocaTurnosEnfermeiro.CountAsync();

                listaPedidosTrocaTurnoEnfermeiros = await pedidoTrocaTurnosEnfermeiro
                    .Include(h => h.Enfermeiro)
                    .Include(h => h.EstadoPedidoTroca)
                    .Include(h => h.HorarioATrocarEnfermeiro.HorarioEnfermeiro)
                    .Include(h => h.HorarioParaTrocaEnfermeiro)
                    .OrderByDescending(h => h.DataPedido)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(nome) && !data.HasValue) //Pesquisa por Nome
            {
                pedidoTrocaTurnosEnfermeiro = _context.PedidoTrocaTurnosEnfermeiros
                    .Where(h => h.Enfermeiro.Nome.Contains(nome.Trim()));

                numHorario = await pedidoTrocaTurnosEnfermeiro.CountAsync();

                listaPedidosTrocaTurnoEnfermeiros = await pedidoTrocaTurnosEnfermeiro
                    .Include(h => h.Enfermeiro)
                    .Include(h => h.EstadoPedidoTroca)
                    .Include(h => h.HorarioATrocarEnfermeiro.HorarioEnfermeiro)
                    .Include(h => h.HorarioParaTrocaEnfermeiro)
                    .OrderByDescending(h => h.DataPedido)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(nome) && data.HasValue) //Pesquisa por nome e data
            {
                int ano = data.Value.Year;
                int mes = data.Value.Month;
                int dia = data.Value.Day;

                pedidoTrocaTurnosEnfermeiro = _context.PedidoTrocaTurnosEnfermeiros
                    .Where(h => h.Enfermeiro.Nome.Contains(nome.Trim()) && h.DataPedido.Year.Equals(ano) && h.DataPedido.Month.Equals(mes) && h.DataPedido.Day.Equals(dia));

                numHorario = await pedidoTrocaTurnosEnfermeiro.CountAsync();

                listaPedidosTrocaTurnoEnfermeiros = await pedidoTrocaTurnosEnfermeiro
                  .Include(h => h.Enfermeiro)
                  .Include(h => h.EstadoPedidoTroca)
                  .Include(h => h.HorarioATrocarEnfermeiro.HorarioEnfermeiro)
                  .Include(h => h.HorarioParaTrocaEnfermeiro)
                  .OrderByDescending(h => h.DataPedido)
                  .Skip(PAGE_SIZE * (page - 1))
                  .Take(PAGE_SIZE)
                  .ToListAsync();
            }
            else
            {
                pedidoTrocaTurnosEnfermeiro = _context.PedidoTrocaTurnosEnfermeiros;

                numHorario = await pedidoTrocaTurnosEnfermeiro.CountAsync();

                listaPedidosTrocaTurnoEnfermeiros = await pedidoTrocaTurnosEnfermeiro
                  .Include(h => h.Enfermeiro)
                    .Include(h => h.EstadoPedidoTroca)
                    .Include(h => h.HorarioATrocarEnfermeiro.HorarioEnfermeiro)
                    .Include(h => h.HorarioParaTrocaEnfermeiro)
                    .OrderByDescending(h => h.DataPedido)
                  .Skip(PAGE_SIZE * (page - 1))
                  .Take(PAGE_SIZE)
                  .ToListAsync();
            }

            if (page > (numHorario / PAGE_SIZE) + 1)
            {
                page = 1;
            }

            if (listaPedidosTrocaTurnoEnfermeiros.Count() == 0)
            {
                TempData["NoItemsFound"] = "Não foram encontrados resultados para a sua pesquisa";
            }


            return View(
                new ListaPedidoTrocaTurnosEnfermeiroViewModel
                {
                    PedidoTrocaTurnosEnfermeiros = listaPedidosTrocaTurnoEnfermeiros,
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

        // GET: PedidoTrocaTurnosEnfermeiros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error));
            }

            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros
                .Include(p => p.Enfermeiro)
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.HorarioATrocarEnfermeiro.HorarioEnfermeiro.Enfermeiro)
                .Include(p => p.HorarioParaTrocaEnfermeiro.HorarioEnfermeiro.Enfermeiro)
                .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosEnfermeiroId == id);
            if (pedidoTrocaTurnosEnfermeiro == null)
            {
                return RedirectToAction(nameof(Error));
            }

            return View(pedidoTrocaTurnosEnfermeiro);
        }

        // GET: PedidoTrocaTurnosEnfermeiros/Create
        public IActionResult Create()
        {
            ViewData["EnfermeiroId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "Nome");
            ViewData["EstadoPedidoTrocaId"] = new SelectList(_context.EstadoPedidoTrocas, "EstadoPedidoTrocaId", "Nome");
            ViewData["HorarioATrocarEnfermeiroId"] = new SelectList(_context.HorarioATrocarEnfermeiros, "HorarioATrocarEnfermeiroId", "HorarioEnfermeiroId");
            ViewData["HorarioParaTrocaEnfermeiroId"] = new SelectList(_context.HorarioParaTrocaEnfermeiros, "HorarioParaTrocaEnfermeiroId", "HorarioEnfermeiroId");
            return View();
        }

        // POST: PedidoTrocaTurnosEnfermeiros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PedidoTrocaTurnosEnfermeiroId,DataPedido,EnfermeiroId,HorarioATrocarEnfermeiroId,HorarioParaTrocaEnfermeiroId,EstadoPedidoTrocaId")] PedidoTrocaTurnosEnfermeiro pedidoTrocaTurnosEnfermeiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidoTrocaTurnosEnfermeiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnfermeiroId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "CC", pedidoTrocaTurnosEnfermeiro.EnfermeiroId);
            ViewData["EstadoPedidoTrocaId"] = new SelectList(_context.EstadoPedidoTrocas, "EstadoPedidoTrocaId", "EstadoPedidoTrocaId", pedidoTrocaTurnosEnfermeiro.EstadoPedidoTrocaId);
            ViewData["HorarioATrocarEnfermeiroId"] = new SelectList(_context.HorarioATrocarEnfermeiros, "HorarioATrocarEnfermeiroId", "HorarioATrocarEnfermeiroId", pedidoTrocaTurnosEnfermeiro.HorarioATrocarEnfermeiroId);
            ViewData["HorarioParaTrocaEnfermeiroId"] = new SelectList(_context.HorarioParaTrocaEnfermeiros, "HorarioParaTrocaEnfermeiroId", "HorarioParaTrocaEnfermeiroId", pedidoTrocaTurnosEnfermeiro.HorarioParaTrocaEnfermeiroId);
            return View(pedidoTrocaTurnosEnfermeiro);
        }

        // GET: PedidoTrocaTurnosEnfermeiros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error));
            }

            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros
                .Include(p => p.Enfermeiro)
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.HorarioATrocarEnfermeiro.HorarioEnfermeiro.Enfermeiro)
                .Include(p => p.HorarioParaTrocaEnfermeiro.HorarioEnfermeiro.Enfermeiro)
                .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosEnfermeiroId == id);

            if (pedidoTrocaTurnosEnfermeiro == null)
            {
                return RedirectToAction(nameof(Error));
            }
       
            return View(pedidoTrocaTurnosEnfermeiro);
        }

        // POST: PedidoTrocaTurnosEnfermeiros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros.FindAsync(id);
           
            if (id != pedidoTrocaTurnosEnfermeiro.PedidoTrocaTurnosEnfermeiroId)
            {
                return RedirectToAction(nameof(Error));
            }

            if (ModelState.IsValid)
            {

                pedidoTrocaTurnosEnfermeiro.EstadoPedidoTrocaId = 4; //Estado_ Validado

                

                try
                {
                    _context.Update(pedidoTrocaTurnosEnfermeiro);    
                    await _context.SaveChangesAsync();
                    TempData["Validated"] = "O pedido foi validado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoTrocaTurnosEnfermeiroExists(pedidoTrocaTurnosEnfermeiro.PedidoTrocaTurnosEnfermeiroId))
                    {
                        return RedirectToAction(nameof(Error));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
           
            return View(pedidoTrocaTurnosEnfermeiro);
        }

        // GET: PedidoTrocaTurnosEnfermeiros/NaoValidar/5
        public async Task<IActionResult> NaoValidar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error));
            }

            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros
                .Include(p => p.Enfermeiro)
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.HorarioATrocarEnfermeiro.HorarioEnfermeiro.Enfermeiro)
                .Include(p => p.HorarioParaTrocaEnfermeiro.HorarioEnfermeiro.Enfermeiro)
                .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosEnfermeiroId == id);

            if (pedidoTrocaTurnosEnfermeiro == null)
            {
                return RedirectToAction(nameof(Error));
            }

            return View(pedidoTrocaTurnosEnfermeiro);
        }

        // POST: PedidoTrocaTurnosEnfermeiros/NaoValidar/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NaoValidar(int id)
        {
            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros.FindAsync(id);

            if (id != pedidoTrocaTurnosEnfermeiro.PedidoTrocaTurnosEnfermeiroId)
            {
                return RedirectToAction(nameof(Error));
            }

            if (ModelState.IsValid)
            {

                EstadoPedidoTroca idEstadoAprovado = _context.EstadoPedidoTrocas.SingleOrDefault(e => e.Nome == "Não Aprovado");

                pedidoTrocaTurnosEnfermeiro.EstadoPedidoTrocaId = idEstadoAprovado.EstadoPedidoTrocaId; //Estado_Não Aprovado

                try
                {
                    _context.Update(pedidoTrocaTurnosEnfermeiro);
                    await _context.SaveChangesAsync();
                    TempData["NotAproved"] = "O pedido foi arquivado!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoTrocaTurnosEnfermeiroExists(pedidoTrocaTurnosEnfermeiro.PedidoTrocaTurnosEnfermeiroId))
                    {
                        return RedirectToAction(nameof(Error));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(pedidoTrocaTurnosEnfermeiro);
        }

        // GET: PedidoTrocaTurnosEnfermeiros/Edit/5
        public async Task<IActionResult> Aprovar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error));
            }

            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros
                .Include(p => p.Enfermeiro)
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.HorarioATrocarEnfermeiro.HorarioEnfermeiro.Enfermeiro)
                .Include(p => p.HorarioParaTrocaEnfermeiro.HorarioEnfermeiro.Enfermeiro)
                .Include(p => p.HorarioATrocarEnfermeiro.HorarioEnfermeiro.Turno)
                .Include(p => p.HorarioParaTrocaEnfermeiro.HorarioEnfermeiro.Turno)
                .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosEnfermeiroId == id);

            if (pedidoTrocaTurnosEnfermeiro == null)
            {
                return RedirectToAction(nameof(Error));
            }

            return View(pedidoTrocaTurnosEnfermeiro);
        }

        // POST: PedidoTrocaTurnosEnfermeiros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Aprovar(int id)
        {

            /*
             * Algoritmo:
             * Alterar o Estado para Aprovado Se O DS aprovar
             * Alterar o Estado para Não Aprovado se o DS não aprovar
             * Caso Aprove, Então:
             * Update na tabela horários, em que as linhas dos horários apenas sofrem a alteração do nome
             * 
             */

            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros.FindAsync(id);

            if (id != pedidoTrocaTurnosEnfermeiro.PedidoTrocaTurnosEnfermeiroId)
            {
                return RedirectToAction(nameof(Error));
            }

            EstadoPedidoTroca idEstadoAprovado = _context.EstadoPedidoTrocas.SingleOrDefault(e => e.Nome == "Aprovado");

            pedidoTrocaTurnosEnfermeiro.EstadoPedidoTrocaId = idEstadoAprovado.EstadoPedidoTrocaId; //Estado_Aprovado

            HorarioATrocarEnfermeiro horario = _context.HorarioATrocarEnfermeiros.SingleOrDefault(h => h.HorarioATrocarEnfermeiroId == pedidoTrocaTurnosEnfermeiro.HorarioATrocarEnfermeiroId);
            HorarioEnfermeiro horarioEnf = _context.HorariosEnfermeiro.SingleOrDefault(h => h.HorarioEnfermeiroId == horario.HorarioEnfermeiroId);

            DateTime dataInicioTurno = horarioEnf.DataInicioTurno;

            if (DataTurnoIsUpperThanDateNow(dataInicioTurno) == true)
            {
                TempData["DateIsUpperThanDateNoe"] = "Já não é possível aprovar o pedido";
            }

            //Update Estado no Pedido de Troca
            try
            {
                //if (!DataTurnoIsUpperThanDateNow(dataInicioTurno))
                //{
                    _context.Update(pedidoTrocaTurnosEnfermeiro);
                    await _context.SaveChangesAsync();
               // }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoTrocaTurnosEnfermeiroExists(pedidoTrocaTurnosEnfermeiro.PedidoTrocaTurnosEnfermeiroId))
                {
                    return RedirectToAction(nameof(Error));
                }
                else
                {
                    throw;
                }
            }

            //Update Horario A Trocar
            int id1 = (from p in _context.PedidoTrocaTurnosEnfermeiros
                      where p.PedidoTrocaTurnosEnfermeiroId == id
                      select p.HorarioATrocarEnfermeiro.HorarioEnfermeiroId).Single();

            HorarioEnfermeiro idHor1 = _context.HorariosEnfermeiro.SingleOrDefault(h => h.HorarioEnfermeiroId == id1);

            var horarioATrocar = await _context.HorariosEnfermeiro.FindAsync(idHor1.HorarioEnfermeiroId);

            int nomeHor1 = (from p in _context.PedidoTrocaTurnosEnfermeiros
                              where p.PedidoTrocaTurnosEnfermeiroId == id
                              select p.HorarioParaTrocaEnfermeiro.HorarioEnfermeiro.Enfermeiro.EnfermeiroId).Single();

            horarioATrocar.EnfermeiroId = nomeHor1;

            //Update Horario Para Troca
            int id2 = (from p in _context.PedidoTrocaTurnosEnfermeiros
                       where p.PedidoTrocaTurnosEnfermeiroId == id
                       select p.HorarioParaTrocaEnfermeiro.HorarioEnfermeiroId).Single();

            HorarioEnfermeiro idHor2 = _context.HorariosEnfermeiro.SingleOrDefault(h => h.HorarioEnfermeiroId == id2);

            var horarioParaTroca = await _context.HorariosEnfermeiro.FindAsync(idHor2.HorarioEnfermeiroId);

            int nomeHor2 = (from p in _context.PedidoTrocaTurnosEnfermeiros
                               where p.PedidoTrocaTurnosEnfermeiroId == id
                               select p.HorarioATrocarEnfermeiro.HorarioEnfermeiro.Enfermeiro.EnfermeiroId).Single();

            horarioParaTroca.EnfermeiroId = nomeHor2;

            try
            {
                UpdateHorario(_context, horarioATrocar);
                UpdateHorario(_context, horarioParaTroca);
                TempData["UpdatedSuccess"] = "Pedido aprovado com sucesso";
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction(nameof(Error));
            }

            return RedirectToAction(nameof(Index));
        }


        /*
         * @param db 
         * @param horario
         * @insert into HorariosEnfermeiro table the horario passed as the parameter
         */
        private void UpdateHorario(HospitalDbContext db, HorarioEnfermeiro horario)
        {
            db.HorariosEnfermeiro.Update(horario);
            db.SaveChanges();
        }

        // GET: PedidoTrocaTurnosEnfermeiros/NaoValidar/5
        public async Task<IActionResult> NaoAprovar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error));
            }

            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros
                .Include(p => p.Enfermeiro)
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.HorarioATrocarEnfermeiro.HorarioEnfermeiro.Enfermeiro)
                .Include(p => p.HorarioParaTrocaEnfermeiro.HorarioEnfermeiro.Enfermeiro)
                .Include(p => p.HorarioATrocarEnfermeiro.HorarioEnfermeiro.Turno)
                .Include(p => p.HorarioParaTrocaEnfermeiro.HorarioEnfermeiro.Turno)
                .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosEnfermeiroId == id);

            if (pedidoTrocaTurnosEnfermeiro == null)
            {
                return RedirectToAction(nameof(Error));
            }

            return View(pedidoTrocaTurnosEnfermeiro);
        }

        // POST: PedidoTrocaTurnosEnfermeiros/NaoValidar/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NaoAprovar(int id)
        {
            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros.FindAsync(id);

            if (id != pedidoTrocaTurnosEnfermeiro.PedidoTrocaTurnosEnfermeiroId)
            {
                return RedirectToAction(nameof(Error));
            }

            if (ModelState.IsValid)
            {

                EstadoPedidoTroca idEstadoAprovado = _context.EstadoPedidoTrocas.SingleOrDefault(e => e.Nome == "Não Aprovado");

                pedidoTrocaTurnosEnfermeiro.EstadoPedidoTrocaId = idEstadoAprovado.EstadoPedidoTrocaId; //Estado_Não Aprovado

                try
                {
                    _context.Update(pedidoTrocaTurnosEnfermeiro);
                    await _context.SaveChangesAsync();
                    TempData["NotAproved"] = "O pedido foi arquivado!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoTrocaTurnosEnfermeiroExists(pedidoTrocaTurnosEnfermeiro.PedidoTrocaTurnosEnfermeiroId))
                    {
                        return RedirectToAction(nameof(Error));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(pedidoTrocaTurnosEnfermeiro);
        }

        // GET: PedidoTrocaTurnosEnfermeiros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error));
            }

            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros
                .Include(p => p.Enfermeiro)
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.HorarioATrocarEnfermeiro)
                .Include(p => p.HorarioParaTrocaEnfermeiro)
                .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosEnfermeiroId == id);
            if (pedidoTrocaTurnosEnfermeiro == null)
            {
                return RedirectToAction(nameof(Error));
            }

            return View(pedidoTrocaTurnosEnfermeiro);
        }

        // POST: PedidoTrocaTurnosEnfermeiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros.FindAsync(id);           
            _context.PedidoTrocaTurnosEnfermeiros.Remove(pedidoTrocaTurnosEnfermeiro);
            await _context.SaveChangesAsync();
            TempData["DeleteRequest"] = "Pedido eliminado com sucesso";
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoTrocaTurnosEnfermeiroExists(int id)
        {
            return _context.PedidoTrocaTurnosEnfermeiros.Any(e => e.PedidoTrocaTurnosEnfermeiroId == id);
        }


        /**********************************Funções Auxiliares*******************************/
        private bool DataTurnoIsUpperThanDateNow(DateTime dataInicioTurno)
        {
            bool IsUpper = false;

            if (dataInicioTurno < DateTime.Now)
            {
                IsUpper = true;
            }

            return IsUpper;
        }
    }
}
