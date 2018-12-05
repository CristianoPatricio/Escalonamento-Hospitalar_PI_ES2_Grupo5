using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class HorariosEnfermeirosViewModel
    {
        public IEnumerable<HorarioEnfermeiro> HorariosEnfermeiros { get; set; }
        public PagingViewModel Pagination { get; set; }

        public string CurrentNome { get; set; }
    }
}
