using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class EnfermeirosEspecialidade
    {
        public int EnfermeiroID { get; set; }
        public Enfermeiros Enfermeiros { get; set; }

        public int EspecilidadeID { get; set; }
        public Especialidade Especialidade { get; set; }

    }
}
