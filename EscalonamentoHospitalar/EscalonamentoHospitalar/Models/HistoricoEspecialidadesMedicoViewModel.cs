using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class HistoricoEspecialidadesMedicoViewModel
    {
        public IEnumerable<MedicoEspecialidade> MedicoEspecialidades { get; set; }
        public PagingViewModel Pagination { get; set; }

        public string CurrentNome { get; set; }
    }
}
