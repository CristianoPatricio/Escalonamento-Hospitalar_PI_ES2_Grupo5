using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class HorarioPaciente
    {
        public int HorarioPacienteId { get; set; }
        public DateTime DataInicio { get; set; }
        public TimeSpan Duracao { get; set; }
        public DateTime DataFim { get; set; }

        public Paciente Paciente { get; set; }
        public int PacienteId { get; set; }

    }
}
