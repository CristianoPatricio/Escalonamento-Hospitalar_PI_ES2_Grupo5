using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class EnfermeiroEspecialidade
    {

        public int EnfermeiroEspecialidadeId { get; set; }
        public string Nome { get; set; }

        public Enfermeiro Enfermeiro { get; set; }
<<<<<<< HEAD

        public int EspecilidadeId { get; set; }
      

=======
        public int EnfermeiroId { get; set; }
>>>>>>> CristianoPatricio
    }
}
