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
    public class PatologiasController : Controller
    {
        private readonly HospitalDbContext _context;

        public PatologiasController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Patologias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Patologia.ToListAsync());
        }

        // GET: Patologias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patologia = await _context.Patologia
                .FirstOrDefaultAsync(m => m.PatologiaId == id);
            if (patologia == null)
            {
                return NotFound();
            }

            return View(patologia);
        }

        // GET: Patologias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patologias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatologiaId,Nome")] Patologia patologia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patologia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //Validar CC através do check digit
            //if (!ValidateNumeroDocumentoCC(nCC))
            {
                //Mensagem de erro se o nº de CC é inválido
                ModelState.AddModelError("CC", "Nº de CC inválido");
            }

            //Validar CC
           // if (ccIsInvalid(nCC))
            {
                //Mensagem de erro se o nº de CC já existe
                ModelState.AddModelError("CC", "Nº de CC já existente");
            }
            return View(patologia);
        }



        // GET: Patologias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patologia = await _context.Patologia.FindAsync(id);
            if (patologia == null)
            {
                return NotFound();
            }
            return View(patologia);
        }

        // POST: Patologias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatologiaId,Nome")] Patologia patologia)
        {
            if (id != patologia.PatologiaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patologia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatologiaExists(patologia.PatologiaId))
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
            return View(patologia);
        }

        // GET: Patologias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patologia = await _context.Patologia
                .FirstOrDefaultAsync(m => m.PatologiaId == id);
            if (patologia == null)
            {
                return NotFound();
            }

            return View(patologia);
        }

        // POST: Patologias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patologia = await _context.Patologia.FindAsync(id);
            _context.Patologia.Remove(patologia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatologiaExists(int id)
        {
            return _context.Patologia.Any(e => e.PatologiaId == id);
        }


                         //Funções Auxiliares

        public bool ValidateNumeroDocumentoCC(string numeroDocumento)
        {
            int sum = 0;
            bool secondDigit = false;
            numeroDocumento = numeroDocumento.ToString().Replace(" ", "");

            if (numeroDocumento.Length != 12)
                throw new ArgumentException("Tamanho inválido para número de documento.");
            for (int i = numeroDocumento.Length - 1; i >= 0; --i)
            {
                int valor = GetNumberFromChar(numeroDocumento[i]);
                if (secondDigit)
                {
                    valor *= 2;
                    if (valor > 9)
                        valor -= 9;
                }
                sum += valor;
                secondDigit = !secondDigit;
            }
            return (sum % 10) == 0;
        }

        public int GetNumberFromChar(char letter)
        {
            switch (letter)
            {
                case '0': return 0;
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                case 'A': return 10;
                case 'B': return 11;
                case 'C': return 12;
                case 'D': return 13;
                case 'E': return 14;
                case 'F': return 15;
                case 'G': return 16;
                case 'H': return 17;
                case 'I': return 18;
                case 'J': return 19;
                case 'K': return 20;
                case 'L': return 21;
                case 'M': return 22;
                case 'N': return 23;
                case 'O': return 24;
                case 'P': return 25;
                case 'Q': return 26;
                case 'R': return 27;
                case 'S': return 28;
                case 'T': return 29;
                case 'U': return 30;
                case 'V': return 31;
                case 'W': return 32;
                case 'X': return 33;
                case 'Y': return 34;
                case 'Z': return 35;
            }
            throw new ArgumentException("Valor inválido no número de documento.");
        }
    }
}
