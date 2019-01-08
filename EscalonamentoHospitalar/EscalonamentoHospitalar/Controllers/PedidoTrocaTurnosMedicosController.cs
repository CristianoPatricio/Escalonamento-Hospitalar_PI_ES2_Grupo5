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
    public class PedidoTrocaTurnosMedicosController : Controller
    {
        private readonly HospitalDbContext _context;

        public PedidoTrocaTurnosMedicosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: PedidoTrocaTurnosMedicos
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.PedidoTrocaTurnosMedico.Include(p => p.EstadoPedidoTroca).Include(p => p.HorarioATrocarMedico).Include(p => p.HorarioParaTrocaMedico).Include(p => p.Medico);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: PedidoTrocaTurnosMedicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoTrocaTurnosMedico = await _context.PedidoTrocaTurnosMedico
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.HorarioATrocarMedico.HorarioMedico)
                .Include(p => p.HorarioParaTrocaMedico.HorarioMedico)
                .Include(p => p.Medico)
                .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosMedicoId == id);
            if (pedidoTrocaTurnosMedico == null)
            {
                return NotFound();
            }

            return View(pedidoTrocaTurnosMedico);
        }

        // GET: PedidoTrocaTurnosMedicos/Create
        public IActionResult Create()
        {
            ViewData["EstadoPedidoTrocaId"] = new SelectList(_context.EstadoPedidoTrocas, "EstadoPedidoTrocaId", "EstadoPedidoTrocaId");
            ViewData["HorarioATrocarMedicoId"] = new SelectList(_context.HorarioATrocarMedico, "HorarioATrocarMedicoId", "HorarioATrocarMedicoId");
            ViewData["HorarioParaTrocaMedicoId"] = new SelectList(_context.HorarioParaTrocaMedico, "HorarioParaTrocaMedicoId", "HorarioParaTrocaMedicoId");
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "CC");
            return View();
        }

        // POST: PedidoTrocaTurnosMedicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PedidoTrocaTurnosMedicoId,DataPedido,MedicoId,HorarioATrocarMedicoId,HorarioParaTrocaMedicoId,EstadoPedidoTrocaId")] PedidoTrocaTurnosMedico pedidoTrocaTurnosMedico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidoTrocaTurnosMedico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoPedidoTrocaId"] = new SelectList(_context.EstadoPedidoTrocas, "EstadoPedidoTrocaId", "EstadoPedidoTrocaId", pedidoTrocaTurnosMedico.EstadoPedidoTrocaId);
            ViewData["HorarioATrocarMedicoId"] = new SelectList(_context.HorarioATrocarMedico, "HorarioATrocarMedicoId", "HorarioATrocarMedicoId", pedidoTrocaTurnosMedico.HorarioATrocarMedicoId);
            ViewData["HorarioParaTrocaMedicoId"] = new SelectList(_context.HorarioParaTrocaMedico, "HorarioParaTrocaMedicoId", "HorarioParaTrocaMedicoId", pedidoTrocaTurnosMedico.HorarioParaTrocaMedicoId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "CC", pedidoTrocaTurnosMedico.MedicoId);
            return View(pedidoTrocaTurnosMedico);
        }

        // GET: PedidoTrocaTurnosMedicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoTrocaTurnosMedico = await _context.PedidoTrocaTurnosMedico
                 .Include(p => p.Medico)
                 .Include(p => p.EstadoPedidoTroca)
                 .Include(p => p.HorarioATrocarMedico.HorarioMedico.Medico)
                 .Include(p => p.HorarioParaTrocaMedico.HorarioMedico.Medico)
                 .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosMedicoId == id);

            if (pedidoTrocaTurnosMedico == null)
            {
                return NotFound();
            }

            return View(pedidoTrocaTurnosMedico);
        }

        // POST: PedidoTrocaTurnosMedicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var pedidoTrocaTurnosMedico = await _context.PedidoTrocaTurnosMedico.FindAsync(id);

            if (id != pedidoTrocaTurnosMedico.PedidoTrocaTurnosMedicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                pedidoTrocaTurnosMedico.EstadoPedidoTrocaId = 4; //Estado_ Validado

                try
                {
                    _context.Update(pedidoTrocaTurnosMedico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoTrocaTurnosMedicoExists(pedidoTrocaTurnosMedico.PedidoTrocaTurnosMedicoId))
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

            return View(pedidoTrocaTurnosMedico);
        }

        // GET: PedidoTrocaTurnosMedicos/NaoValidar/5
        public async Task<IActionResult> NaoValidar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoTrocaTurnosMedico = await _context.PedidoTrocaTurnosMedico
                .Include(p => p.Medico)
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.HorarioATrocarMedico.HorarioMedico.Medico)
                .Include(p => p.HorarioParaTrocaMedico.HorarioMedico.Medico)
                .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosMedicoId == id);

            if (pedidoTrocaTurnosMedico == null)
            {
                return NotFound();
            }

            return View(pedidoTrocaTurnosMedico);
        }

        // POST: PedidoTrocaTurnosMedicos/NaoValidar/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NaoValidar(int id)
        {
            var pedidoTrocaTurnosMedico = await _context.PedidoTrocaTurnosMedico.FindAsync(id);

            if (id != pedidoTrocaTurnosMedico.PedidoTrocaTurnosMedicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                EstadoPedidoTroca idEstadoAprovado = _context.EstadoPedidoTrocas.SingleOrDefault(e => e.Nome == "Não Aprovado");

                pedidoTrocaTurnosMedico.EstadoPedidoTrocaId = idEstadoAprovado.EstadoPedidoTrocaId; //Estado_Não Aprovado

                try
                {
                    _context.Update(pedidoTrocaTurnosMedico);
                    await _context.SaveChangesAsync();
                    TempData["NotAproved"] = "O pedido foi arquivado!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoTrocaTurnosMedicoExists(pedidoTrocaTurnosMedico.PedidoTrocaTurnosMedicoId))
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

            return View(pedidoTrocaTurnosMedico);
        }

        // GET: PedidoTrocaTurnosMedicos/Edit/5
        public async Task<IActionResult> Aprovar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoTrocaTurnosMedico = await _context.PedidoTrocaTurnosMedico
                .Include(p => p.Medico)
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.HorarioATrocarMedico.HorarioMedico.Medico)
                .Include(p => p.HorarioParaTrocaMedico.HorarioMedico.Medico)
                .Include(p => p.HorarioATrocarMedico.HorarioMedico.Turno)
                .Include(p => p.HorarioParaTrocaMedico.HorarioMedico.Turno)
                .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosMedicoId == id);

            if (pedidoTrocaTurnosMedico == null)
            {
                return NotFound();
            }

            return View(pedidoTrocaTurnosMedico);
        }

        // POST: PedidoTrocaTurnosMedicos/Edit/5
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

            var pedidoTrocaTurnosMedico = await _context.PedidoTrocaTurnosMedico.FindAsync(id);

            if (id != pedidoTrocaTurnosMedico.PedidoTrocaTurnosMedicoId)
            {
                return NotFound();
            }

            EstadoPedidoTroca idEstadoAprovado = _context.EstadoPedidoTrocas.SingleOrDefault(e => e.Nome == "Aprovado");

            pedidoTrocaTurnosMedico.EstadoPedidoTrocaId = idEstadoAprovado.EstadoPedidoTrocaId; //Estado_Aprovado

            //Update Estado no Pedido de Troca
            try
            {
                _context.Update(pedidoTrocaTurnosMedico);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoTrocaTurnosMedicoExists(pedidoTrocaTurnosMedico.PedidoTrocaTurnosMedicoId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //Update Horario A Trocar
            int id1 = (from p in _context.PedidoTrocaTurnosMedico
                       where p.PedidoTrocaTurnosMedicoId == id
                       select p.HorarioATrocarMedico.HorarioMedicoId).Single();

            HorarioMedico idHor1 = _context.HorariosMedicos.SingleOrDefault(h => h.HorarioMedicoId == id1);

            var horarioATrocar = await _context.HorariosMedicos.FindAsync(idHor1.HorarioMedicoId);

            int nomeHor1 = (from p in _context.PedidoTrocaTurnosMedico
                            where p.PedidoTrocaTurnosMedicoId == id
                            select p.HorarioParaTrocaMedico.HorarioMedico.Medico.MedicoId).Single();

            horarioATrocar.MedicoId = nomeHor1;

            //Update Horario Para Troca
            int id2 = (from p in _context.PedidoTrocaTurnosMedico
                       where p.PedidoTrocaTurnosMedicoId == id
                       select p.HorarioParaTrocaMedico.HorarioMedicoId).Single();

            HorarioMedico idHor2 = _context.HorariosMedicos.SingleOrDefault(h => h.HorarioMedicoId == id2);

            var horarioParaTroca = await _context.HorariosMedicos.FindAsync(idHor2.HorarioMedicoId);

            int nomeHor2 = (from p in _context.PedidoTrocaTurnosMedico
                            where p.PedidoTrocaTurnosMedicoId == id
                            select p.HorarioATrocarMedico.HorarioMedico.Medico.MedicoId).Single();

            horarioParaTroca.MedicoId = nomeHor2;

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
        private void UpdateHorario(HospitalDbContext db, HorarioMedico horario)
        {
            db.HorariosMedicos.Update(horario);
            db.SaveChanges();
        }

        // GET: PedidoTrocaTurnosEnfermeiros/NaoValidar/5
        public async Task<IActionResult> NaoAprovar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoTrocaTurnosEnfermeiro = await _context.PedidoTrocaTurnosMedico
                .Include(p => p.Medico)
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.HorarioATrocarMedico.HorarioMedico.Medico)
                .Include(p => p.HorarioParaTrocaMedico.HorarioMedico.Medico)
                .Include(p => p.HorarioATrocarMedico.HorarioMedico.Turno)
                .Include(p => p.HorarioParaTrocaMedico.HorarioMedico.Turno)
                .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosMedicoId == id);

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
            var pedidoTrocaTurnosMedico = await _context.PedidoTrocaTurnosMedico.FindAsync(id);

            if (id != pedidoTrocaTurnosMedico.PedidoTrocaTurnosMedicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                EstadoPedidoTroca idEstadoAprovado = _context.EstadoPedidoTrocas.SingleOrDefault(e => e.Nome == "Não Aprovado");

                pedidoTrocaTurnosMedico.EstadoPedidoTrocaId = idEstadoAprovado.EstadoPedidoTrocaId; //Estado_Não Aprovado

                try
                {
                    _context.Update(pedidoTrocaTurnosMedico);
                    await _context.SaveChangesAsync();
                    TempData["NotAproved"] = "O pedido foi arquivado!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoTrocaTurnosMedicoExists(pedidoTrocaTurnosMedico.PedidoTrocaTurnosMedicoId))
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

            return View(pedidoTrocaTurnosMedico);
        }


        // GET: PedidoTrocaTurnosMedicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoTrocaTurnosMedico = await _context.PedidoTrocaTurnosMedico
                .Include(p => p.EstadoPedidoTroca)
                .Include(p => p.HorarioATrocarMedico)
                .Include(p => p.HorarioParaTrocaMedico)
                .Include(p => p.Medico)
                .FirstOrDefaultAsync(m => m.PedidoTrocaTurnosMedicoId == id);
            if (pedidoTrocaTurnosMedico == null)
            {
                return NotFound();
            }

            return View(pedidoTrocaTurnosMedico);
        }

        // POST: PedidoTrocaTurnosMedicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedidoTrocaTurnosMedico = await _context.PedidoTrocaTurnosMedico.FindAsync(id);
            _context.PedidoTrocaTurnosMedico.Remove(pedidoTrocaTurnosMedico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoTrocaTurnosMedicoExists(int id)
        {
            return _context.PedidoTrocaTurnosMedico.Any(e => e.PedidoTrocaTurnosMedicoId == id);
        }
    }
}
