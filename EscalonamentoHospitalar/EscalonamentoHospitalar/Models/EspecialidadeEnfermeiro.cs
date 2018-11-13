using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class EspecialidadeEnfermeiro
    {
        public int EspecialidadeEnfermeiroId { get; set; }
        public string Especialidade { get; set; }
    
        public ICollection<Enfermeiro> Enfermeiro { get; set; }
        public ICollection<EnfermeiroEspecialidade> EnfermeirosEspecialidade { get; set; }
    }
}
