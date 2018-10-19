using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EscalonamentoHospitalar.Models
{
    public class Pacientes
    {
        public int Pacientes_ID { get; set; }

        public string Nome { get; set; }

        public string Morada { get; set; }

        public string Cod_Postal { get; set; }

        public string CC { get; set; }

        public string Numero_Utente { get; set; }

        public string Telefone {get; set;}

      

    }
}