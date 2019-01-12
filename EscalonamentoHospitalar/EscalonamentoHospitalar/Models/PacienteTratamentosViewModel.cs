using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class PacienteTratamentosViewModel
    {
        public IEnumerable<Paciente> Pacientes { get; set; }
        public IEnumerable<Tratamento> Tratamentos { get; set; }
    }
}
