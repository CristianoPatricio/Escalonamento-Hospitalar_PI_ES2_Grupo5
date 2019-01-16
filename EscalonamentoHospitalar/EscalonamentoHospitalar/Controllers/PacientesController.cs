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
    [Authorize] 
    public class PacientesController : Controller
    {
        private const int PAGE_SIZE = 5;
        private readonly HospitalDbContext _context;

        public PacientesController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Pacientes
        public async Task<IActionResult> Index(ListaPacientesViewModel model = null, int page = 1)
        {
            string nome = null;

            if (model != null && model.CurrentNome != null)
            {
                nome = model.CurrentNome;
                page = 1;
            }

            var pacientes = _context.Pacientes
                .Where(e => nome == null || e.Nome.Contains(nome));

            int numPacientes = await pacientes.CountAsync();

            if (page > (numPacientes / PAGE_SIZE) + 1)
            {
                page = 1;
            }

            var listaPaciente = await pacientes
                .OrderBy(e => e.Nome)
                .Skip(PAGE_SIZE * (page - 1))
                .Take(PAGE_SIZE)
                .ToListAsync();

            return View(
                new ListaPacientesViewModel
                {
                    Pacientes = listaPaciente,
                    Pagination = new PagingViewModel
                    {
                        CurrentPage = page,
                        PageSize = PAGE_SIZE,
                        TotalItems = numPacientes
                    },
                    CurrentNome = nome
                }
            );

        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.PacienteId == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PacienteId,Nome,Morada,Cod_Postal,Email,CC,Data_Nascimento,Numero_Utente,Contacto")] Paciente paciente)
        {

            
            DateTime pacienteBDate = paciente.Data_Nascimento;
            
            

            var nCC = paciente.CC;

            //Validar Data de Nascimento do Paciente
            if (pacienteDateIsInvalid(pacienteBDate) == true)
            {
                //Mensagem de erro se a data de nascimento do enfermeiro for inválida
                ModelState.AddModelError("Data_Nascimento", "Data de nascimento inválida");
            }
            //Validar CC através do check digit
            if (!ValidateNumeroDocumentoCC(nCC))
            {
                //Mensagem de erro se o nº de CC é inválido
                ModelState.AddModelError("CC", "Nº de CC inválido");
            }

            //Validar CC
            if (ccIsInvalid(nCC))
            {
                //Mensagem de erro se o nº de CC já existe
                ModelState.AddModelError("CC", "Nº de CC já existente");
            }


            if (ModelState.IsValid)
            {
                if (!pacienteDateIsInvalid(pacienteBDate) || ValidateNumeroDocumentoCC(nCC))
                {
                    _context.Add(paciente);
                    await _context.SaveChangesAsync();
                    TempData["notice"] = "Registo inserido com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
            }


            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PacienteId,Nome,Morada,Cod_Postal,Email,CC,Data_Nascimento,Numero_Utente,Contacto")] Paciente paciente)
        {
            var nCC = paciente.CC;
            DateTime pacienteBDate = paciente.Data_Nascimento;
            var PacienteId = paciente.PacienteId;


            //Validar Data de Nascimento do Paciente
            if (pacienteDateIsInvalid(pacienteBDate) == true)
            {
                //Mensagem de erro se a data de nascimento do paciente for inválida
                ModelState.AddModelError("Data_Nascimento", "Data de nascimento inválida");
            }
            //Validar CC através do check digit
            if (!ValidateNumeroDocumentoCC(nCC))
            {
                //Mensagem de erro se o nº de CC é inválido
                ModelState.AddModelError("CC", "Nº de CC inválido");
            }

        

            //Validar CC
            if (ccIsInvalidEdit(nCC,PacienteId))
            
            {
                //Mensagem de erro se o CC já existir
                ModelState.AddModelError("CC", "Nº de CC já existente");
            }
            if (id != paciente.PacienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    try
                    {
                    if (!pacienteDateIsInvalid(pacienteBDate) || ValidateNumeroDocumentoCC(nCC))
                    {
                        _context.Update(paciente);
                        await _context.SaveChangesAsync();
                        TempData["successEdit"] = "Registo alterado com sucesso";
                    }
                    }
                    
                
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.PacienteId))
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
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.PacienteId == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            TempData["deleteEnf"] = "Paciente eliminado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        /**********************Funções auxiliares**************************/

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

        private bool ccIsInvalid(string cc)
        {
            bool IsInvalid = false;


            //Procura na BD se existem enfermeiros com o mesmo numero mecanografico
            var pacientes = from e in _context.Pacientes
                              where e.CC.Contains(cc)
                              select e;

            if (!pacientes.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        /*para a Data de Nascimento
          birth is invalid    */
        private bool pacienteDateIsInvalid(DateTime Data_Nascimento)
        {
            bool IsInvalid = false;
            DateTime dateNow = DateTime.Now;

            int dateTimeCompare = DateTime.Compare(Data_Nascimento, dateNow);

            if (dateTimeCompare > 0) 
            {
                IsInvalid = true;
            }
            return IsInvalid;
        }

        /**
        * @param cc
        * @param idEnf
        * @return true if the cc already exists in DB  
        */
        private bool ccIsInvalidEdit(string cc, int PacienteId)
        {
            bool IsInvalid = false;


            
            var pacientes = from e in _context.Pacientes
                              where e.CC.Contains(cc) && e.PacienteId != PacienteId
                              select e;

            if (!pacientes.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.PacienteId == id);
        }
    }
}
