using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class EnfermeiroEspecialidade
    {
        public int EnfermeiroEspecialidadeId { get; set; }

        public int EnfermeiroId { get; set; }
        public Enfermeiro Enfermeiro { get; set; }

        public int EspecilidadeId { get; set; }
      

    }
}
