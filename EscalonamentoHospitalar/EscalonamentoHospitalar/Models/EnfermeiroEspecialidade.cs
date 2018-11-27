using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class EnfermeiroEspecialidade
    {

        public int EspecialidadeEnfermeiroId { get; set; }
        public EspecialidadeEnfermeiro EspecialidadeEnfermeiro { get; set; }

<<<<<<< HEAD
        public Enfermeiro Enfermeiro { get; set; }

=======
>>>>>>> CristianoPatricio
        public int EnfermeiroId { get; set; }
        public Enfermeiro Enfermeiro { get; set; }

        public DateTime Data_Registo { get; set; }
       
    }
}
