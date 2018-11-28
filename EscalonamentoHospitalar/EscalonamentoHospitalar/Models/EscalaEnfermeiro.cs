using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class EscalaEnfermeiro
    {
        public int EscalaEnfermeiroId { get; set; }

        // O TURNO ESTÁ DEPENDENTE DO CRISTIANO
         public Turno Turno { get; set; }
         public int TurnoId { get; set; }
        
        // O ENFERMEIRO ESTÁ DEPENDENTE DO CRISTIANO
        public Enfermeiro Enfermeiro { get; set; }
        public int EnfermeiroId { get; set; }
    }
}
