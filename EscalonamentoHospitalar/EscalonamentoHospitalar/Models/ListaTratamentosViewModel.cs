using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class ListaTratamentosViewModel
    {
        public IEnumerable<Tratamento> Tratamentos { get; set; }

        public IEnumerable<Paciente> Pacientes { get; set; }

        public PagingViewModel Pagination { get; set; }

        public string CurrentNome { get; set; }
    }
}
