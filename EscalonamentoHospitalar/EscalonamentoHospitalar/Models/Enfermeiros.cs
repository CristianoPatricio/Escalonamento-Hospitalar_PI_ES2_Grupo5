using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EscalonamentoHospitalar.Models
{
    public class Enfermeiros 
    {
        public string EnfermeiroID{ get; set; }

        //Numero da ordem 
        public string NumeroMecanografico{ get; set; }

        public string Nome { get; set; }

        public string Especialidade { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public DateTime Data_Nascimento { get; set; }

        [RegularExpression(@"\d{8}(\s\d{1})?", ErrorMessage = "Cartão de Cidadão Inválido")]
        public string CC { get; set; }




    }
}

