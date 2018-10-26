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

        [StringLength(30, ErrorMessage = "O nome é demasiado comprido.", MinimumLength = 6)]
        [Required(ErrorMessage = "Introduza o nome")]
        public string Nome { get; set; }

        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Morada Inválida")]
        public string Morada { get; set; }


        [RegularExpression(@"\d{4}(-\d{3})?", ErrorMessage = "Código Postal Inválido")]
        public string Cod_Postal { get; set; }

        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})", ErrorMessage = "Email Inválido")]
        public string Email { get; set; }

        [RegularExpression(@"\d{8}(\s\d{1})?", ErrorMessage = "Cartão de Cidadão Inválido")]
        public string CC { get; set; }

        [Required(ErrorMessage = "Por indroduza a data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Data_Nascimento { get; set; }

        [RegularExpression(@"(\d{9})", ErrorMessage = "Nº Utente de Saúde Inválido")]
        public string Numero_Utente { get; set; }

        [RegularExpression(@"(2\d{8})|(9[1236]\d{7})", ErrorMessage = "Contacto Inválido")]
        [Required(ErrorMessage = "Por favor indroduza o contacto")]
        public string Contacto {get; set;}

        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Nome Inválido")]
        public string BoletimClinico { get; set; }



      

    }
}