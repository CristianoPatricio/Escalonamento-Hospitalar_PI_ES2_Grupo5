using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class MedicoEspecialidade
    {
        public int MedicoEspecialidadeId { get; set; }
        public string Nome { get; set; }

        public int MedicoId { get; set; }
        public Medico Medico { get; set; }

    }
}
