using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class MedicosEspecialidade
    {
        public int MedicoID { get; set; }
        public Medicos Medicos { get; set; }

        public int EspecialidadeId { get; set; }
        public Especialidade Especialidade { get; set; }


    }
}
