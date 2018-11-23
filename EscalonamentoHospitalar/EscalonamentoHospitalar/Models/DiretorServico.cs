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

        [Required(ErrorMessage = "Por favor, introduza o código")]
        [RegularExpression(@"[D]\d+", ErrorMessage = "Código inválido")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o nome")]
        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Nome inválido")]
        public string Nome { get; set; }

        [RegularExpression(@"(2\d{8})|(9[1236]\d{7})", ErrorMessage = "Contacto inválido")]
        public string Contacto { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o email")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o nº de Cartão de Cidadão")]
        [RegularExpression(@"(\d{8}\s\d{1}[A-Z0-9]{2}\d{1})", ErrorMessage = "Nº de Cartão de Cidadão inválido")]
        public string CC { get; set; }    
        }
    }