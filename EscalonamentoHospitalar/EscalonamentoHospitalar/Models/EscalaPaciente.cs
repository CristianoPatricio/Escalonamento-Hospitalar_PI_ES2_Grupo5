using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class EscalaPaciente
    {
        public int EscalaPacienteId { get; set; }

        // O TRATAMENTO ESTÁ DEPENDENTE DO NELSON
        public Tratamento Tratamento { get; set; }
          public int TratamentoId { get; set; }

        public Paciente Paciente { get; set; }
        public int PacienteId { get; set; }

    }
}
