using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EscalonamentoHospitalar.Models
{
    public class HistoricoTratamentosViewModel
    {

        public IEnumerable<Tratamento> Tratamentos { get; set; }
        public PagingViewModel Pagination { get; set; }

        public string CurrentNome { get; set; }
    }
}