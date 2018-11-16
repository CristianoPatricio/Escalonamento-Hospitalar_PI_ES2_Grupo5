using System;
using System.Collections.Generic;
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

    }
}
