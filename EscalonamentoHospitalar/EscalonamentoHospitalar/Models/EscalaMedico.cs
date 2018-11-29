using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class EscalaMedico
    {
        public int EscalaMedicoId { get; set; }

        // O TURNO ESTÁ DEPENDENTE DO CRISTIANO
        public Turno Turno { get; set; }
        public int TurnoId { get; set; }

        public Medico Medico { get; set; }
        public int MedicoId { get; set; }

        [Required(ErrorMessage = "Por favor indroduza a data de inicio da semana")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Data_Inicio_Semana { get; set; }
    }
}
