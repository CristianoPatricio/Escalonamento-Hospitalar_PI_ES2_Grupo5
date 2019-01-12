using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class HistoricoTratamento
    {
    public int HistoricoTratamentoId { get; set; }

    public Tratamento Tratamento { get; set; }
        public int TratamentoId { get; set; }

        public Paciente Paciente { get; set; }
        public int PacienteId { get; set; }

        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }


    }
}