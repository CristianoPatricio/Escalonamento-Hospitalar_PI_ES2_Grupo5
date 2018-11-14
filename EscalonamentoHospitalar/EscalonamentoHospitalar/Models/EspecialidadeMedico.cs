using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class EspecialidadeMedico
    {
        public int EspecialidadeMedicoId { get; set; }
        
        public ICollection<Medico> Medico { get; set; }
        public ICollection<MedicoEspecialidade> MedicosEspecialidade { get; set; }
    }
}
