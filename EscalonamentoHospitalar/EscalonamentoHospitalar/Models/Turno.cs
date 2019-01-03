using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class Turno
    {
        public int TurnoId { get; set; }
        public string Nome { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime HoraInicio { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime HoraFim { get; set; }

        public ICollection<HorarioEnfermeiro> HorariosEnfermeiro { get; set; }
        public ICollection<HorarioMedico> HorariosMedico { get; set; }
    }
}
