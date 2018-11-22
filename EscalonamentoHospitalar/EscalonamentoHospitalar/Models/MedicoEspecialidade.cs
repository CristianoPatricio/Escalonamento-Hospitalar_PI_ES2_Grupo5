using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class MedicoEspecialidade
    {
       
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }

        public int EspecialidadeMedicoId { get; set; }       
        public EspecialidadeMedico EspecialidadeMedico { get; set; }

        [Required(ErrorMessage = "Por favor indroduza a data de registo da especialidade")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Data_Registo { get; set; }

    }
}
