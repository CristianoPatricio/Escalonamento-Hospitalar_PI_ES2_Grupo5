using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})", ErrorMessage = "Email Inválido")]
        public string Email { get; set; }

        [RegularExpression(@"\d{8}(\s\d{1})?", ErrorMessage = "Cartão de Cidadão Inválido")]
        public string CC { get; set; }

        public DateTime Data_Nascimento { get; set; }

        public string Numero_Utente { get; set; }

        public string Telefone {get; set;}

        public string BoletimClinico { get; set; }



      

    }
}