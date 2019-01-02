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
        private readonly HospitalDbContext _context;

        public PedidoTrocaTurnosEnfermeirosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: PedidoTrocaTurnosEnfermeiros
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.PedidoTrocaTurnosEnfermeiros
                .Include(p => p.Enfermeiro)
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.HorarioATrocarEnfermeiro.HorarioEnfermeiro)
                .Include(p => p.HorarioParaTrocaEnfermeiro.HorarioEnfermeiro);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: PedidoTrocaTurnosEnfermeiros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros
                .Include(p => p.Enfermeiro)
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.HorarioATrocarEnfermeiro.HorarioEnfermeiro.Enfermeiro)
                .Include(p => p.HorarioParaTrocaEnfermeiro.HorarioEnfermeiro.Enfermeiro)
                .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosEnfermeiroId == id);
            if (pedidoTrocaTurnosEnfermeiro == null)
            {
                return NotFound();
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
                return NotFound();
            }

            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros
                .Include(p => p.Enfermeiro)
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.HorarioATrocarEnfermeiro.HorarioEnfermeiro.Enfermeiro)
                .Include(p => p.HorarioParaTrocaEnfermeiro.HorarioEnfermeiro.Enfermeiro)
                .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosEnfermeiroId == id);

            if (pedidoTrocaTurnosEnfermeiro == null)
            {
                return NotFound();
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
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                pedidoTrocaTurnosEnfermeiro.EstadoPedidoTrocaId = 4; //Estado_ Validado

                try
                {
                    _context.Update(pedidoTrocaTurnosEnfermeiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoTrocaTurnosEnfermeiroExists(pedidoTrocaTurnosEnfermeiro.PedidoTrocaTurnosEnfermeiroId))
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
           
            return View(pedidoTrocaTurnosEnfermeiro);
        }

        // GET: PedidoTrocaTurnosEnfermeiros/NaoValidar/5
        public async Task<IActionResult> NaoValidar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros
                .Include(p => p.Enfermeiro)
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.HorarioATrocarEnfermeiro.HorarioEnfermeiro.Enfermeiro)
                .Include(p => p.HorarioParaTrocaEnfermeiro.HorarioEnfermeiro.Enfermeiro)
                .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosEnfermeiroId == id);

            if (pedidoTrocaTurnosEnfermeiro == null)
            {
                return NotFound();
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
                return NotFound();
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
                        return NotFound();
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
                return NotFound();
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
                return NotFound();
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
                return NotFound();
            }

            EstadoPedidoTroca idEstadoAprovado = _context.EstadoPedidoTrocas.SingleOrDefault(e => e.Nome == "Aprovado");

            pedidoTrocaTurnosEnfermeiro.EstadoPedidoTrocaId = idEstadoAprovado.EstadoPedidoTrocaId; //Estado_Aprovado

            //Update Estado no Pedido de Troca
            try
            {
                _context.Update(pedidoTrocaTurnosEnfermeiro);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoTrocaTurnosEnfermeiroExists(pedidoTrocaTurnosEnfermeiro.PedidoTrocaTurnosEnfermeiroId))
                {
                    return NotFound();
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
                return NotFound();
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
                return NotFound();
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
                return NotFound();
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
                return NotFound();
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
                        return NotFound();
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
                return NotFound();
            }

            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosEnfermeiros
                .Include(p => p.Enfermeiro)
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.HorarioATrocarEnfermeiro)
                .Include(p => p.HorarioParaTrocaEnfermeiro)
                .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosEnfermeiroId == id);
            if (pedidoTrocaTurnosEnfermeiro == null)
            {
                return NotFound();
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
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoTrocaTurnosEnfermeiroExists(int id)
        {
            return _context.PedidoTrocaTurnosEnfermeiros.Any(e => e.PedidoTrocaTurnosEnfermeiroId == id);
        }
    }
}
