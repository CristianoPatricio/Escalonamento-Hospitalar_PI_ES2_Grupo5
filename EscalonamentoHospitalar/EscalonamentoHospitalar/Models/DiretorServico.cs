using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EscalonamentoHospitalar.Models
{
    public class DiretorServico
    {
            public int DiretorServicoID { get; set; }

            public string Name { get; set; }
                     
            //Numero da Ordem
            public string NumeroMecanografico { get; set; }

            public string Telefone { get; set; }

            [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})", ErrorMessage = "Email Inválido")]
             public string Email { get; set; }

            [RegularExpression(@"\d{8}(\s\d{1})?", ErrorMessage = "Cartão de Cidadão Inválido")]
            public string CC { get; set; }

            public string Morada { get; set; }


        }
    }