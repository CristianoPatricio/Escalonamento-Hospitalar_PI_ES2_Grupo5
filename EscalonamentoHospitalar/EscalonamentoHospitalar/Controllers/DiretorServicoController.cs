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
    public class DiretorServicoController : Controller
    {
        private readonly HospitalDbContext _context;

        public DiretorServicoController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: DiretorServico
        public async Task<IActionResult> Index()
        {
            return View(await _context.DiretoresServico.ToListAsync());
        }

        // GET: DiretorServico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diretorServico = await _context.DiretoresServico
                .FirstOrDefaultAsync(m => m.DiretorServicoID == id);
            if (diretorServico == null)
            {
                return NotFound();
            }

            return View(diretorServico);
        }

        // GET: DiretorServico/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DiretorServico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiretorServicoID,Codigo,Nome,Contacto,Email,CC")] DiretorServico diretorServico)
        {

            var numero = diretorServico.Codigo;
            var email = diretorServico.Email;
            var nCC = diretorServico.CC;
            var contacto = diretorServico.Contacto;

            //Validar Numero Mecanografico         
            if (numMecIsInvalid(numero) == true)
            {
                //Mensagem de erro se o codigo for inválido
                ModelState.AddModelError("Codigo", "Este código já existe");
            }

            //Validar Email           
            if (emailIsInvalid(email) == true)
            {
                //Mensagem de erro se o email for inválido
                ModelState.AddModelError("Email", "Este email já existe");
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

            //Validar contacto
            if (contactoIsInvalid(contacto))
            {
                //Mensagem de erro se o contacto já existe
                ModelState.AddModelError("Contacto", "Contacto já existente");
            }


            if (ModelState.IsValid)
            {
                if (!contactoIsInvalid(contacto) || !numMecIsInvalid(numero) || !emailIsInvalid(email) || !ccIsInvalid(nCC) || ValidateNumeroDocumentoCC(nCC))
                {
                    _context.Add(diretorServico);
                    await _context.SaveChangesAsync();
                    TempData["insertSuccess"] = "Registo inserido com sucesso!";
                    return RedirectToAction(nameof(Index));
                }              
            }
            return View(diretorServico);
        }

        // GET: DiretorServico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diretorServico = await _context.DiretoresServico.FindAsync(id);
            if (diretorServico == null)
            {
                return NotFound();
            }
            return View(diretorServico);
        }

        // POST: DiretorServico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiretorServicoID,Codigo,Nome,Contacto,Email,CC")] DiretorServico diretorServico)
        {

            var numero = diretorServico.Codigo;
            var email = diretorServico.Email;
            var nCC = diretorServico.CC;
            var idDir = diretorServico.DiretorServicoID;
            var contacto = diretorServico.Contacto;

            //Validar Numero Mecanografico         
            if (numMecIsInvalidEdit(numero, idDir) == true)
            {
                //Mensagem de erro se o número mecanográfico for inválido
                ModelState.AddModelError("Codigo", "Este código já existe");
            }

            //Validar Email           
            if (emailIsInvalidEdit(email, idDir) == true)
            {
                //Mensagem de erro se o email for inválido
                ModelState.AddModelError("Email", "Este email já existe");
            }

            //Validar CC através do check digit
            if (!ValidateNumeroDocumentoCC(nCC))
            {
                //Mensagem de erro se o nº de CC é inválido
                ModelState.AddModelError("CC", "Nº de CC inválido");
            }

            //Validar CC
            if (ccIsInvalidEdit(nCC, idDir))
            {
                //Mensagem de erro se o nº de CC já existe
                ModelState.AddModelError("CC", "Nº de CC já existente");
            }

            //Validar Contacto
            if (contactoIsInvalidEdit(contacto, idDir))
            {
                //Mensagem de erro se o contacto já existe
                ModelState.AddModelError("Contacto", "Contacto já existente");
            }


            if (id != diretorServico.DiretorServicoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!contactoIsInvalidEdit(contacto, idDir) || !ccIsInvalidEdit(nCC, idDir) || !emailIsInvalidEdit(email, idDir) || !numMecIsInvalidEdit(numero, idDir) || ValidateNumeroDocumentoCC(nCC))
                    {
                        _context.Update(diretorServico);
                        await _context.SaveChangesAsync();
                        TempData["successEdit"] = "Registo alterado com sucesso";
                    }
                  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiretorServicoExists(diretorServico.DiretorServicoID))
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
            return View(diretorServico);
        }

        // GET: DiretorServico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diretorServico = await _context.DiretoresServico
                .FirstOrDefaultAsync(m => m.DiretorServicoID == id);
            if (diretorServico == null)
            {
                return NotFound();
            }

            return View(diretorServico);
        }

        // POST: DiretorServico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diretorServico = await _context.DiretoresServico.FindAsync(id);
            _context.DiretoresServico.Remove(diretorServico);
            await _context.SaveChangesAsync();
            TempData["deleteSuccess"] = "Registo eliminado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool DiretorServicoExists(int id)
        {
            return _context.DiretoresServico.Any(e => e.DiretorServicoID == id);
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

        /**
       * @param email
       * @return true if the email already exists in DB  
       */
        private bool emailIsInvalid(string email)
        {
            bool IsInvalid = false;

            //Procura na BD se existem diretores com o mesmo email
            var diretores = from d in _context.DiretoresServico
                              where d.Email.Contains(email)
                              select d;

            if (!diretores.Count().Equals(0))
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


            //Procura na BD se existem diretores com o mesmo codigo
            var diretores = from d in _context.DiretoresServico
                              where d.Codigo.Contains(numero)
                              select d;

            if (!diretores.Count().Equals(0))
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



            //Procura na BD se existem diretores com o mesmo numero mecanografico
            var diretores = from d in _context.DiretoresServico

            //Procura na BD se existem diretores com o mesmo CC
            var diretores = from d in _context.DiretorServico
                              where d.CC.Contains(cc)
                              select d;

            if (!diretores.Count().Equals(0))
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

            //Procura na BD se existem diretores com o mesmo contacto
            var diretores = from d in _context.DiretorServico
                              where d.Contacto.Contains(contacto)
                              select d;

            if (!diretores.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        /**************************EDIT**************************/

        /**
       * @param email
       * @param idDir
       * @return true if the email already exists in DB  
       */
        private bool emailIsInvalidEdit(string email, int idDir)
        {
            bool IsInvalid = false;

            //Procura na BD se existem diretores com o mesmo email
            var diretores = from d in _context.DiretoresServico
                            where d.Email.Contains(email) && d.DiretorServicoID != idDir
                            select d;

            if (!diretores.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        /**
        * @param numero
        * @param idDir
        * @return true if the numero already exists in DB  
        */
        private bool numMecIsInvalidEdit(string numero, int idDir)
        {
            bool IsInvalid = false;


            //Procura na BD se existem diretores com o mesmo codigo
            var diretores = from d in _context.DiretoresServico
                            where d.Codigo.Contains(numero) && d.DiretorServicoID != idDir
                            select d;

            if (!diretores.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

        /**
        * @param cc
        * @param idDir
        * @return true if the cc already exists in DB  
        */
        private bool ccIsInvalidEdit(string cc, int idDir)
        {
            bool IsInvalid = false;



            //Procura na BD se existem diretores com o mesmo numero mecanografico
            var diretores = from d in _context.DiretoresServico

            //Procura na BD se existem diretores com o mesmo CC
            var diretores = from d in _context.DiretorServico
                            where d.CC.Contains(cc) && d.DiretorServicoID != idDir
                            select d;

            if (!diretores.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }


        // GET: DiretorServico/GestaoHospitalar
        public IActionResult GestaoHospitalar()
        {
            return View();
        }


        /**
       * @param contacto
       * @return true if the contacto already exists in DB  
       */
        private bool contactoIsInvalidEdit(string contacto, int idDir)
        {
            bool IsInvalid = false;

            //Procura na BD se existem diretores com o mesmo contacto
            var diretores = from d in _context.DiretorServico
                            where d.Contacto.Contains(contacto) && d.DiretorServicoID != idDir
                            select d;

            if (!diretores.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }

    }
}
