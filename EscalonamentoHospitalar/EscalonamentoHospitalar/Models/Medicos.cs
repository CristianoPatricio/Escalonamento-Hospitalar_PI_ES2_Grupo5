using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EscalonamentoHospitalar.Models
{
    public class Medicos : Controller
    {
        public string MedicoID { get; set; }

        public string Codigo_Medico { get; set; }

        public string Nome { get; set; }

        public string Telemovel { get; set; }

        public string Email { get; set; }

        public string Morada { get; set; }

        public string Cod_Postal { get; set; }
    }
}