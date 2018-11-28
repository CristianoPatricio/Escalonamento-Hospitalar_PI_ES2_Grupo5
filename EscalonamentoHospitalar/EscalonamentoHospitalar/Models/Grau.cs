using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EscalonamentoHospitalar.Models
{
    public class Grau {
        public int GrauId { get; set; }

        public string TipoGrau { get; set; }

        public ICollection<Tratamento> Tratamentos { get; set; }
    }
}