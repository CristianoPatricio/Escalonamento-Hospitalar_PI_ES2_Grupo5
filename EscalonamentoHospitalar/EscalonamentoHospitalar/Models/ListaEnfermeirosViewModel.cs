using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class ListaEnfermeirosViewModel
    {
        public IEnumerable<Enfermeiro> Enfermeiros { get; set; }
        public PagingViewModel Pagination { get; set; }

        public string CurrentNome { get; set; }
    }
}
