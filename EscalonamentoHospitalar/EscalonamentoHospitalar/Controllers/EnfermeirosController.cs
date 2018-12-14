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
    [Authorize(Policy = "AcessoRestritoDiretorServico")] // Política de acesso restrito ao Diretor de Servico
    public class EnfermeirosController : Controller
    {
        private const int PAGE_SIZE = 5;
        private readonly HospitalDbContext _context;

        public EnfermeirosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Enfermeiros
        public async Task<IActionResult> Index(ListaEnfermeirosViewModel model = null, int page = 1)
        {
            string nome = null;

            if (model != null && model.CurrentNome != null)
            {
                nome = model.CurrentNome;
                page = 1;
            }

            var enfermeiros = _context.Enfermeiros
                .Where(e => nome == null || e.Nome.Contains(nome));

            int numEnfermeiros = await enfermeiros.CountAsync();

            if (page > (numEnfermeiros / PAGE_SIZE) + 1)
            {
                page = 1;
            }

            var listaEnfermeiro = await enfermeiros
                .Include(e => e.EspecialidadeEnfermeiro)
                .OrderBy(e => e.Nome)
                .Skip(PAGE_SIZE * (page - 1))
                .Take(PAGE_SIZE)
                .ToListAsync();

            return View(
                new ListaEnfermeirosViewModel
                {
                    Enfermeiros = listaEnfermeiro,
                    Pagination = new PagingViewModel
                    {
                        CurrentPage = page,
                        PageSize = PAGE_SIZE,          
                        TotalItems = numEnfermeiros
                    },
                    CurrentNome = nome
                }
            );
        }

        // GET: Enfermeiros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiro = await _context.Enfermeiros
                .Include(e => e.EspecialidadeEnfermeiro)
                .FirstOrDefaultAsync(m => m.EnfermeiroId == id);
            if (enfermeiro == null)
            {
                return NotFound();
            }

            return View(enfermeiro);
        }

        // GET: Enfermeiros/Create
        public IActionResult Create()
        {
                                                                                                                                         //Apresenta Nome da Especialidade 
            ViewData["EspecialidadeEnfermeiroId"] = new SelectList(_context.Set<EspecialidadeEnfermeiro>(), "EspecialidadeEnfermeiroId", "Especialidade");
            return View();
        }

        // POST: Enfermeiros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnfermeiroId,NumeroMecanografico,Nome,EspecialidadeEnfermeiroId,Contacto,Email,Data_Nascimento,CC,Filhos,Data_Nascimento_Filho")] Enfermeiro enfermeiro)
        {

            /********************************/
            DateTime dateNow = DateTime.Now;
            DateTime enfermeiroBDate = enfermeiro.Data_Nascimento;
            var numero = enfermeiro.NumeroMecanografico;
            var email = enfermeiro.Email;
            var nCC = enfermeiro.CC;
            var contacto = enfermeiro.Contacto;

            bool sonBDateIsInvalid = false;
         
            //Validar Numero Mecanografico         
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

            //Validar Data de Nascimento do Enfermeiro
            if (enfDateIsInvalid(enfermeiroBDate) == true)
            {        
                //Mensagem de erro se a data de nascimento do enfermeiro for inválida
                ModelState.AddModelError("Data_Nascimento", "Data de nascimento inválida");
            }

             //Validar Data de Nascimento do Filho + Novo
            if (enfermeiro.Data_Nascimento_Filho != null)
            {
                DateTime sonDate = (DateTime)enfermeiro.Data_Nascimento_Filho;
               
                if (sonDateIsInvalid(sonDate) == true)
                {
                    sonBDateIsInvalid = true;
                    //Mensagem de erro se a data de nascimento do filho for inválida
                    ModelState.AddModelError("Data_Nascimento_Filho", "Data de nascimento inválida");
                }
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

            //Validar Contacto
            if (contactoIsInvalid(contacto))
            {
                //Mensagem de erro se o nº de CC já existe
                ModelState.AddModelError("Contacto", "Contacto já existente");
            }

            /********************************/           

            if (ModelState.IsValid)
            {
                if (!contactoIsInvalid(contacto) || !sonBDateIsInvalid || !numMecIsInvalid(numero) || !emailIsInvalid(email) || !enfDateIsInvalid(enfermeiroBDate) || ValidateNumeroDocumentoCC(nCC))
                {
                    _context.Add(enfermeiro);

                    //Inserir Registo na Tabela EnfermeiroEspecialidades
                    InsertDataIntoEnfermeiroEspecialidades(_context, enfermeiro.EspecialidadeEnfermeiroId, enfermeiro.EnfermeiroId);

                    await _context.SaveChangesAsync();
                    TempData["notice"] = "Registo inserido com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                           
            }
            ViewData["EspecialidadeEnfermeiroId"] = new SelectList(_context.Set<EspecialidadeEnfermeiro>(), "EspecialidadeEnfermeiroId", "Especialidade", enfermeiro.EspecialidadeEnfermeiroId);
            return View(enfermeiro);
        }
       
        // GET: Enfermeiros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiro = await _context.Enfermeiros.FindAsync(id);
            if (enfermeiro == null)
            {
                return NotFound();
            }
            ViewData["EspecialidadeEnfermeiroId"] = new SelectList(_context.Set<EspecialidadeEnfermeiro>(), "EspecialidadeEnfermeiroId", "Especialidade", enfermeiro.EspecialidadeEnfermeiroId);
            return View(enfermeiro);
        }

        // POST: Enfermeiros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnfermeiroId,NumeroMecanografico,Nome,EspecialidadeEnfermeiroId,Contacto,Email,Data_Nascimento,CC,Filhos,Data_Nascimento_Filho")] Enfermeiro enfermeiro)
        {
            /*************************************/

            DateTime enfermeiroBDate = enfermeiro.Data_Nascimento;
            var nCC = enfermeiro.CC;
            var numero = enfermeiro.NumeroMecanografico;
            var idEnf = enfermeiro.EnfermeiroId;
            var email = enfermeiro.Email;
            var idEsp = enfermeiro.EspecialidadeEnfermeiroId;
            var contacto = enfermeiro.Contacto;

            bool sonBDateIsInvalid = false;

            if (id != enfermeiro.EnfermeiroId)
            {
                return NotFound();
            }

            //Validar Numero Mecanografico
            if (numMecIsInvalidEdit(numero, idEnf) == true){
                //Mensagem de erro se o número mecanográfico já existir
                ModelState.AddModelError("NumeroMecanografico", "Este número já existe");
            }

            //Validar Data de Nascimento do Enfermeiro
            if (enfDateIsInvalid(enfermeiroBDate) == true)
            {
                //Mensagem de erro se a data de nascimento do enfermeiro for inválida
                ModelState.AddModelError("Data_Nascimento", "Data de nascimento inválida");
            }

            //Validar Data de Nascimento do Filho + Novo
            if (enfermeiro.Data_Nascimento_Filho != null)
            {
                DateTime sonDate = (DateTime)enfermeiro.Data_Nascimento_Filho;
               
                if (sonDateIsInvalid(sonDate) == true)
                {
                    sonBDateIsInvalid = true;
                    //Mensagem de erro se a data de nascimento do filho for inválida
                    ModelState.AddModelError("Data_Nascimento_Filho", "Data de nascimento inválida");
                }
            }

            //Validar CC através do check digit
            if (!ValidateNumeroDocumentoCC(nCC))
            {
                //Mensagem de erro se o nº de CC é inválido
                ModelState.AddModelError("CC", "Nº de CC inválido");
            }

            //Validar Email
            if (emailIsInvalidEdit(email, idEnf))
            {
                //Mensagem de erro se o email já existir
                ModelState.AddModelError("Email", "Email já existente");
            }

            //Validar CC
            if (ccIsInvalidEdit(nCC, idEnf))
            {
                //Mensagem de erro se o CC já existir
                ModelState.AddModelError("CC", "Nº de CC já existente");
            }

            //Validar Contacto
            if (contactoIsInvalidEdit(contacto, idEnf))
            {
                //Mensagem de erro se o CC já existir
                ModelState.AddModelError("Contacto", "Contacto já existente");
            }

            /*******************************************/

            if (ModelState.IsValid)
            {
                try
                {                   
                    if (!contactoIsInvalidEdit(contacto, idEnf) || !ccIsInvalidEdit(nCC, idEnf) || !emailIsInvalidEdit(email, idEnf) || !numMecIsInvalidEdit(numero, idEnf) || !enfDateIsInvalid(enfermeiroBDate) || !sonBDateIsInvalid || ValidateNumeroDocumentoCC(nCC))
                    {
                        _context.Update(enfermeiro);

                        //Verifica na tabela EnfermeiroEspecialidade se já existe o registo
                        if (insertNewDataIntoEnfEspecialidade(idEnf, idEsp)){
                            InsertDataIntoEnfermeiroEspecialidades(_context, enfermeiro.EspecialidadeEnfermeiroId, enfermeiro.EnfermeiroId);
                        }

                        await _context.SaveChangesAsync();
                        TempData["successEdit"] = "Registo alterado com sucesso";
                    }
                                  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnfermeiroExists(enfermeiro.EnfermeiroId))
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
            ViewData["EspecialidadeEnfermeiroId"] = new SelectList(_context.Set<EspecialidadeEnfermeiro>(), "EspecialidadeEnfermeiroId", "Especialidade", enfermeiro.EspecialidadeEnfermeiroId);
            return View(enfermeiro);
        }

        // GET: Enfermeiros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiro = await _context.Enfermeiros
                .Include(e => e.EspecialidadeEnfermeiro)
                .FirstOrDefaultAsync(m => m.EnfermeiroId == id);
            if (enfermeiro == null)
            {
                return NotFound();
            }

            return View(enfermeiro);
        }

        // POST: Enfermeiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enfermeiro = await _context.Enfermeiros.FindAsync(id);
            _context.Enfermeiros.Remove(enfermeiro);
            await _context.SaveChangesAsync();
            TempData["deleteEnf"] = "Enfermeiro eliminado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool EnfermeiroExists(int id)
        {
            return _context.Enfermeiros.Any(e => e.EnfermeiroId == id);
        }

        /**********************Funções auxiliares**************************/

        public bool ValidateNumeroDocumentoCC(string numeroDocumento)
        {
            int sum = 0;
            bool secondDigit = false;
            numeroDocumento = numeroDocumento.ToString().Replace(" ","");

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

        private void InsertDataIntoEnfermeiroEspecialidades(HospitalDbContext db, int especialidade, int enfermeiro)
        {
            DateTime date = DateTime.Now;

            db.EnfermeirosEspecialidades.Add(
                new EnfermeiroEspecialidade { EspecialidadeEnfermeiroId = especialidade, EnfermeiroId = enfermeiro, Data_Registo = date }
            );

            db.SaveChanges();
        }

      
        /**
         * @param enfDate
         * @return true if nurse´s date of birth is invalid   
         */
        private bool enfDateIsInvalid(DateTime enfDate)
        {
            bool IsInvalid = false;
            DateTime dateNow = DateTime.Now;

            if (dateNow.Year - enfDate.Year <= 22)
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        /**
         * @param sonDate
         * @return true if son´s date of birth is invalid   
         */
        private bool sonDateIsInvalid(DateTime sonDate)
        {
            bool IsInvalid = false;
            DateTime dateNow = DateTime.Now;          

            int dateTimeCompare = DateTime.Compare(sonDate, dateNow);

            if (dateTimeCompare > 0) //son's date of birth is later than date now
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

            //Procura na BD se existem enfermeiros com o mesmo email
            var enfermeiros = from e in _context.Enfermeiros
                              where e.Email.Contains(email)
                              select e;

            if (!enfermeiros.Count().Equals(0))
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


            //Procura na BD se existem enfermeiros com o mesmo numero mecanografico
            var enfermeiros = from e in _context.Enfermeiros
                              where e.NumeroMecanografico.Contains(numero)
                              select e;

            if (!enfermeiros.Count().Equals(0))
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


            //Procura na BD se existem enfermeiros com o mesmo CC
            var enfermeiros = from e in _context.Enfermeiros
                              where e.CC.Contains(cc)
                              select e;

            if (!enfermeiros.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }


        /**
        * @param contacto
        * @return true if the contacto already exists in DB  
        */
        private bool contactoIsInvalid(string contacto)
        {
            bool IsInvalid = false;


            //Procura na BD se existem enfermeiros com o mesmo contacto
            var enfermeiros = from e in _context.Enfermeiros
                              where e.Contacto.Contains(contacto)
                              select e;

            if (!enfermeiros.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }


        /*************************EDIT*******************************/

        /**
        * @param numero
        * @param idEnf
        * @return true if the numero already exists in DB  
        */
        private bool numMecIsInvalidEdit(string numero, int idEnf)
        {
            bool IsInvalid = false;


            //Procura na BD se existem enfermeiros com o mesmo numero mecanografico
            var enfermeiros = from e in _context.Enfermeiros
                              where e.NumeroMecanografico.Contains(numero) && e.EnfermeiroId != idEnf
                              select e;

            if (!enfermeiros.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        /**
       * @param email
       * @param idEnf
       * @return true if the email already exists in DB  
       */
        private bool emailIsInvalidEdit(string email, int idEnf)
        {
            bool IsInvalid = false;

            //Procura na BD se existem enfermeiros com o mesmo email
            var enfermeiros = from e in _context.Enfermeiros
                              where e.Email.Contains(email) && e.EnfermeiroId != idEnf
                              select e;

            if (!enfermeiros.Count().Equals(0))
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
        private bool ccIsInvalidEdit(string cc, int idEnf)
        {
            bool IsInvalid = false;


            //Procura na BD se existem enfermeiros com o mesmo numero mecanografico
            var enfermeiros = from e in _context.Enfermeiros
                              where e.CC.Contains(cc) && e.EnfermeiroId != idEnf
                              select e;

            if (!enfermeiros.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        /**
        * @param idEnf
        * @param idEsp
        * @return true if the number of records equals 0. A new record will be inserted in DB.  
        */
        private bool insertNewDataIntoEnfEspecialidade(int idEnf, int idEsp)
        {
            bool insert = false;

            //Procura na BD se existem enfermeiroespecialidade com o mesmo numero mecanografico e especialidade
            var enfermeirosEspecialidades = from ee in _context.EnfermeirosEspecialidades
                              where ee.EnfermeiroId == idEnf && ee.EspecialidadeEnfermeiroId == idEsp
                              select ee;

            if (enfermeirosEspecialidades.Count().Equals(0))
            {
                insert = true;
            }


            return insert;
        }

        /**
       * @param contacto
       * @return true if the contacto already exists in DB  
       */
        private bool contactoIsInvalidEdit(string contacto, int idEnf)
        {
            bool IsInvalid = false;


            //Procura na BD se existem enfermeiros com o mesmo contacto
            var enfermeiros = from e in _context.Enfermeiros
                              where e.Contacto.Contains(contacto) && e.EnfermeiroId != idEnf
                              select e;

            if (!enfermeiros.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

    }
}
