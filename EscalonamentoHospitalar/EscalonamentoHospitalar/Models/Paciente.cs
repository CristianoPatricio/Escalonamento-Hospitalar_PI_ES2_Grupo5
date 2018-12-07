using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EscalonamentoHospitalar.Models
{
    public class Paciente
    {
        
        public int PacienteId { get; set; }

        [Required(ErrorMessage = "Nome Inválido")]
        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Nome Inválido")]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Morada Inválida")]
        public string Morada { get; set; }

        [Required(ErrorMessage = "Codigo Postal Inválido")]
        [RegularExpression(@"\d{4}(-\d{3})?", ErrorMessage = "Código Postal Inválido")]
        public string Cod_Postal { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o email")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o nº de CC/BI")]
        [RegularExpression(@"(\d{8}\s\d{1}[A-Z0-9]{2}\d{1})", ErrorMessage = "Nº de CC/BI inválido")]
        public string CC { get; set; }

        [Required(ErrorMessage = "Por indroduza a data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Data_Nascimento { get; set; }

        [Required(ErrorMessage = "Numero Inválido")]
        [RegularExpression(@"(\d{9})", ErrorMessage = "Nº Utente de Saúde Inválido")]
        public string Numero_Utente { get; set; }

        [RegularExpression(@"(2\d{8})|(9[1236]\d{7})", ErrorMessage = "Contacto Inválido")]
        [Required(ErrorMessage = "Por favor indroduza o contacto")]
        public string Contacto {get; set;}



        public ICollection<Tratamento> Tratamentos { get; set; }




    }
}