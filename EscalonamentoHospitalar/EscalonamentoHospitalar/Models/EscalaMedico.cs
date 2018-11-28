using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class EscalaMedico
    {
        public int EscalaMedicoId { get; set; }

        // O TURNO ESTÁ DEPENDENTE DO CRISTIANO
        public Turno Turno { get; set; }
        public int TurnoId { get; set; }

        public Medico Medico { get; set; }
        public int MedicoId { get; set; }
    }
}
