﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class Patologia
    {
        public int PatologiaId { get; set; }

        public string Nome { get; set; }

        public ICollection<Tratamento> Tratamentos { get; set; }
    }
}
