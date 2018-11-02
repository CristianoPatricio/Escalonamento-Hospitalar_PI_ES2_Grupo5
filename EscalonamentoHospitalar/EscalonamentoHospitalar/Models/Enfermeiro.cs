using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EscalonamentoHospitalar.Models
{
    public class Enfermeiro
    {
        [Key]
        public int EnfermeiroID{ get; set; }

        [RegularExpression(@"\d{7}(\s\d{1})?", ErrorMessage = "Numero Errado")]
        //Numero da ordem 
        public string NumeroMecanografico{ get; set; }

        [Required(ErrorMessage = "Por favor indroduza o nome")]
        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Nome Inválido")]
        public string Nome { get; set; }

        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Especialidade Invalida")]
        public string Especialidade { get; set; }

        [RegularExpression(@"(2\d{8})|(9[1236]\d{7})", ErrorMessage = "Contacto Inválido")]
        public string Contacto { get; set; }

        [Required(ErrorMessage = "Por favor indroduza o email")]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})", ErrorMessage = "Email Inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por indroduza a data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Data_Nascimento { get; set; }

        [RegularExpression(@"\d{8}(\s\d{1})?", ErrorMessage = "Cartão de Cidadão Inválido")]
        public string CC { get; set; }

      //  public int EspecialidadeId { get; set; }
      public ICollection<EnfermeiroEspecialidade> EnfermeiroEspecialidade { get; set; }

    }
}

