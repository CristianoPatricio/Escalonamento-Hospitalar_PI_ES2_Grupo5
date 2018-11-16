using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EscalonamentoHospitalar.Models
{
    public class Tratamento
    {
        public int TratamentoId { get; set; }

        //[RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Patologia Inválida")]
        public Patologia Patologia { get; set; }

        public int PatologiaId { get; set; }

        public Paciente Paciente { get; set; }
        public int PacienteId { get; set; }

        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Grau Inválido")]
        public Grau Grau { get; set; }

        public int GrauId { get; set; }

        [Required(ErrorMessage = "Por introduza a data de Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataInicio {get;set;}

        [Required(ErrorMessage = "Por introduza a data de Fim")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "Por favor introduza a Duração do Ciclo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "DD:YYYY 00:00", ApplyFormatInEditMode = false)]
        public string DuracaoCiclo { get; set; }

        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Regime Inválido")]

        public Regime Regime { get; set; }
        public int RegimeId { get; set; }

        public bool Decorrer { get; set; }

        public bool Concluido { get; set; }

        public Medico Medico { get; set; }
        public int MedicoId { get; set; }


    }
}