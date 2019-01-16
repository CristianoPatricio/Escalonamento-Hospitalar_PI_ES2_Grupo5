using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class GerarHorarioMedico
    {
        [Required(ErrorMessage = "Por favor, introduza o número de pessoas para o turno da manhã")]
        [RegularExpression(@"(^[1-9]\d*$)", ErrorMessage = "Por favor, insira um número positivo, maior que zero")]
        public int NumeroPessoasTurno1 { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o número de pessoas para o turno da tarde")]
        [RegularExpression(@"(^[1-9]\d*$)", ErrorMessage = "Por favor, insira um número positivo, maior que zero")]
        public int NumeroPessoasTurno2 { get; set; }

        [Required(ErrorMessage = "Por favor, introduza a data de ínicio da semana")]
        [DataType(DataType.Date, ErrorMessage = "Data de início de semana inválida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataInicioSemana { get; set; }
    }
}
