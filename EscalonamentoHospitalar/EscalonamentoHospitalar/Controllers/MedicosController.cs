using EscalonamentoHospitalar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Controllers
{
    [Authorize(Policy = "AcessoRestritoDiretorServico")] // Política de acesso restrito ao Diretor de Serviço
    public class MedicosController : Controller

    {
        private const int PAGE_SIZE = 5;
        private readonly HospitalDbContext _context;

        public MedicosController(HospitalDbContext context)

        {
            _context = context;
        }

        // GET: Medicos
        public async Task<IActionResult> Index(ListaMedicosViewModel model = null, int page = 1)
        {
            string nome = null;

            if (model != null && model.CurrentNome != null)
            {
                nome = model.CurrentNome;
                page = 1;
            }

            var medicos = _context.Medicos
                .Where(e => nome == null || e.Nome.Contains(nome));

            int numMedicos = await medicos.CountAsync();

            if (page > (numMedicos / PAGE_SIZE) + 1)
            {
                page = 1;
            }

            var listaMedico = await medicos
                .Include(e => e.EspecialidadeMedico)
                .OrderBy(e => e.Nome)
                .Skip(PAGE_SIZE * (page - 1))
                .Take(PAGE_SIZE)
                .ToListAsync();

            return View(
                new ListaMedicosViewModel
                {
                    Medicos = listaMedico,
                    Pagination = new PagingViewModel
                    {
                        CurrentPage = page,
                        PageSize = PAGE_SIZE,
                        TotalItems = numMedicos
                    },
                    CurrentNome = nome
                }
            );

        }

        // GET: Medicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .Include(m => m.EspecialidadeMedico)
                .FirstOrDefaultAsync(m => m.MedicoId == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medicos/Create
        public IActionResult Create()
        {
            ViewData["EspecialidadeMedicoId"] = new SelectList(_context.Set<EspecialidadeMedico>(), "EspecialidadeMedicoId", "NomeEspecialidade");
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicoId,NumeroMecanografico,Nome,Email,Contacto,CC,Data_Nascimento,EspecialidadeMedicoId,Data_Inicio_Servico")] Medico medico)
        {

            /**************VALIDAÇÕES******************/

            DateTime dateNow = DateTime.Now;
            DateTime medicoBDate = medico.Data_Nascimento;
           
            var numero = medico.NumeroMecanografico;
            var email = medico.Email;
            var nCC = medico.CC;

            bool medicoISDateIsInvalid = false;

            // Validar Numero Mecanografico         
            if (numMecIsInvalid(numero) == true)
            {
                //Mensagem de erro se o número mecanográfico for inválido
                ModelState.AddModelError("NumeroMecanografico", "Este número já existe");
            }

            //Validar Email           
            if (emailIsInvalid(email) == true)
            {
                //Mensagem de erro se o email for inválido
                ModelState.AddModelError("Email", "Este email já existe");
            }

            //Validar Data de Nascimento do Médico
            if (medicoDateIsInvalid(medicoBDate) == true)
            {
                //Mensagem de erro se a data de nascimento do médico for inválida
                ModelState.AddModelError("Data_Nascimento", "Data de nascimento inválida");
            }

            //Validar CC através do check digit
            if (!ValidateNumeroDocumentoCC(nCC))
            {
                //Mensagem de erro se o nº de CC é inválido
                ModelState.AddModelError("CC", "Nº de Cartão de Cidadão inválido");
            }

            //Validar CC
            if (ccIsInvalid(nCC))
            {
                //Mensagem de erro se o nº de CC já existe
                ModelState.AddModelError("CC", "Nº de Cartão de Cidadão já existente");
            }

            //Validar Data de Inicio de Serviço do Médico        
            DateTime medicoISDate = (DateTime)medico.Data_Inicio_Servico;

             if (medicoInSerDateIsInvalid(medicoISDate, medicoBDate) == true)
             {
                 medicoISDateIsInvalid = true;
                 //Mensagem de erro se a data de inicio de serviço do médico for inválida
                 ModelState.AddModelError("Data_Inicio_Servico", "Data de inicio de serviço inválida");
             }
            

            /********************************/

            if (ModelState.IsValid)
            {
                if (!numMecIsInvalid(numero) || !emailIsInvalid(email) || !medicoDateIsInvalid(medicoBDate) || ValidateNumeroDocumentoCC(nCC) || !medicoISDateIsInvalid)
                {
                    _context.Add(medico);

                    //Inserir Registo na Tabela MédicoEspecialidades
                    InsertDataIntoMedicoEspecialidades(_context, medico.EspecialidadeMedicoId, medico.MedicoId);

                    await _context.SaveChangesAsync();
                    TempData["notice"] = "Registo inserido com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["EspecialidadeMedicoId"] = new SelectList(_context.Set<EspecialidadeMedico>(), "EspecialidadeMedicoId", "NomeEspecialidade", medico.EspecialidadeMedicoId);
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
            { 
                return NotFound();
            }

            ViewData["EspecialidadeMedicoId"] = new SelectList(_context.Set<EspecialidadeMedico>(), "EspecialidadeMedicoId", "NomeEspecialidade", medico.EspecialidadeMedicoId);
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicoId,NumeroMecanografico,Nome,Email,Contacto,CC,Data_Nascimento,EspecialidadeMedicoId,Data_Inicio_Servico")] Medico medico)
        {

            /***************VALIDAÇÕES**********************/

            DateTime medicoBDate = medico.Data_Nascimento;
            DateTime medicoISDate = medico.Data_Inicio_Servico;
            var nCC = medico.CC;
            var numero = medico.NumeroMecanografico;
            var idMedico = medico.MedicoId;
            var email = medico.Email;
            var idEspecialidade = medico.EspecialidadeMedicoId;

            bool medicoISDateIsInvalid = false;


            if (id != medico.MedicoId)
            {
                return NotFound();
            }

            //Validar Numero Mecanografico
            if (numMecIsInvalidEdit(numero, idMedico) == true)
            {
                //Mensagem de erro se o número mecanográfico já existir
                ModelState.AddModelError("NumeroMecanografico", "Este número já existe");
            }

            //Validar Data de Nascimento do médico
            if (medicoDateIsInvalid(medicoBDate) == true)
            {
                //Mensagem de erro se a data de nascimento do médico for inválida
                ModelState.AddModelError("Data_Nascimento", "Data de nascimento inválida");
            }

            //Validar CC através do check digit
            if (!ValidateNumeroDocumentoCC(nCC))
            {
                //Mensagem de erro se o nº de CC é inválido
                ModelState.AddModelError("CC", "Nº de Cartão de Cidadão inválido");
            }

            //Validar Email
            if (emailIsInvalidEdit(email, idMedico))
            {
                //Mensagem de erro se o email já existir
                ModelState.AddModelError("Email", "Email já existente");
            }

            //Validar CC
            if (ccIsInvalidEdit(nCC, idMedico))
            {
                //Mensagem de erro se o CC já existir
                ModelState.AddModelError("CC", "Nº de Cartão de Cidadão já existente");
            }

            //Validar Data de inicio de serviço do médico
            
               

                if (medicoInSerDateIsInvalid(medicoISDate, medicoBDate) == true)
                {
                    medicoISDateIsInvalid = true;
                    //Mensagem de erro se a data de inicio de serviço do médico for inválida
                    ModelState.AddModelError("Data_Inicio_Servico", "Data de inicio de serviço inválida");
                }
            

            /*******************************************/
            if (ModelState.IsValid)
            {
                try
                {
                   
                    if (!ccIsInvalidEdit(nCC, idMedico) || !emailIsInvalidEdit(email, idMedico) || !numMecIsInvalidEdit(numero, idMedico) || !medicoDateIsInvalid(medicoBDate) || ValidateNumeroDocumentoCC(nCC) || !medicoISDateIsInvalid)
                    {
                        _context.Update(medico);

                        //Verifica na tabela MedicoEspecialidade se já existe o registo
                        if (insertNewDataIntoMedicoEspecialidade(idMedico, idEspecialidade))
                        {
                            InsertDataIntoMedicoEspecialidades(_context, medico.EspecialidadeMedicoId, medico.MedicoId);
                        }

                        await _context.SaveChangesAsync();
                        TempData["successEdit"] = "Registo alterado com sucesso";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.MedicoId))
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

            ViewData["EspecialidadeMedicoId"] = new SelectList(_context.Set<EspecialidadeMedico>(), "EspecialidadeMedicoId", "NomeEspecialidade", medico.EspecialidadeMedicoId);
            return View(medico);
        }
        
        // GET: Medicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
            .Include(m => m.EspecialidadeMedico)
            .FirstOrDefaultAsync(m => m.MedicoId == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
         
        } 

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            _context.Medicos.Remove(medico);
            await _context.SaveChangesAsync();
            TempData["deleteMedico"] = "Médico eliminado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)   
        {
            return _context.Medicos.Any(e => e.MedicoId == id);
        }

        /**********************Funções auxiliares das validações**************************/

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

        private void InsertDataIntoMedicoEspecialidades(HospitalDbContext db, int especialidade, int medico)
        {
            DateTime date = DateTime.Now;

            db.MedicoEspecialidades.Add(
                new MedicoEspecialidade { EspecialidadeMedicoId = especialidade, MedicoId = medico, Data_Registo = date }
            );

            db.SaveChanges();
        }


        /**
         * @param medicoDate
         * @return true if doctor´s date of birth is invalid   
         */
        private bool medicoDateIsInvalid(DateTime medicoDate)
        {
            bool IsInvalid = false;
            DateTime dateNow = DateTime.Now;

            if (dateNow.Year - medicoDate.Year <= 24)
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        /**
        * @param email
        * @return true if the email already exists in DB  
        */
        private bool emailIsInvalid(string email)
        {
            bool IsInvalid = false;

            //Procura na BD se existem medicos com o mesmo email
            var medicos = from m in _context.Medicos
                              where m.Email.Contains(email)
                              select m;

            if (!medicos.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        /**
        * @param numero
        * @return true if the numero already exists in DB  
        */
        private bool numMecIsInvalid(string numero)
        {
            bool IsInvalid = false;


            //Procura na BD se existem medicos com o mesmo numero mecanografico
            var medicos = from m in _context.Medicos
                              where m.NumeroMecanografico.Contains(numero)
                              select m;

            if (!medicos.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        /**
        * @param cc
        * @return true if the cc already exists in DB  
        */
        private bool ccIsInvalid(string cc)
        {
            bool IsInvalid = false;

            //Procura na BD se já exite o número de cc
            var medicos = from m in _context.Medicos
                              where m.CC.Contains(cc)
                              select m;

            if (!medicos.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        /**
                 * @param medicoISDate
                 * @return true if doctor´s date of start of service is invalid   
                 */
        private bool medicoInSerDateIsInvalid(DateTime medicoISDate, DateTime medicoBDate)
        {
            bool IsInvalid = false;

            DateTime dateNow = DateTime.Now;

            int dateTimeCompare = DateTime.Compare(medicoISDate, dateNow);
            int dateTimeCompare2 = DateTime.Compare(medicoISDate, medicoBDate);

            if (dateTimeCompare > 0 || dateTimeCompare2 <= 0 || (medicoISDate.Year - medicoBDate.Year <= 24)) //doctor's date of start of service is later than date now
            {
                IsInvalid = true;
            }
            return IsInvalid;
        }


        /*************************EDIT*******************************/

        /**
        * @param numero
        * @param idMedico
        * @return true if the numero already exists in DB  
        */
        private bool numMecIsInvalidEdit(string numero, int idMedico)
        {
            bool IsInvalid = false;


            //Procura na BD se existem médicos com o mesmo numero mecanografico
            var medicos = from m in _context.Medicos
                              where m.NumeroMecanografico.Contains(numero) && m.MedicoId != idMedico
                              select m;

            if (!medicos.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        /**
       * @param email
       * @param idMedico
       * @return true if the email already exists in DB  
       */
        private bool emailIsInvalidEdit(string email, int idMedico)
        {
            bool IsInvalid = false;

            //Procura na BD se existem médicos com o mesmo email
            var medicos = from m in _context.Medicos
                              where m.Email.Contains(email) && m.MedicoId != idMedico
                              select m;

            if (!medicos.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        /**
        * @param cc
        * @param idMedico
        * @return true if the cc already exists in DB  
        */
        private bool ccIsInvalidEdit(string cc, int idMedico)
        {
            bool IsInvalid = false;


            //Procura na BD se existe o numero de cc
            var medicos = from m in _context.Medicos
                              where m.CC.Contains(cc) && m.MedicoId != idMedico
                              select m;

            if (!medicos.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        /**
        * @param idMedico
        * @param idEspecialidade
        * @return true if the number of records equals 0. A new record will be inserted in DB.  
        */
        private bool insertNewDataIntoMedicoEspecialidade(int idMedico, int idEspecialidade)
        {
            bool insert = false;

            //Procura na BD se existem medicoespecialidade com o mesmo numero mecanografico e especialidade
            var medicoEspecialidades = from mm in _context.MedicoEspecialidades
                                            where mm.MedicoId == idMedico && mm.EspecialidadeMedicoId == idEspecialidade
                                            select mm;

            if (medicoEspecialidades.Count().Equals(0))
            {
                insert = true;
            }


            return insert;
        }

    }
}
 