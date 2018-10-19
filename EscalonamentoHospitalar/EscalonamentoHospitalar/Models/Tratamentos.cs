using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EscalonamentoHospitalar.Models
{
    public class Tratamentos
    {
        public int Tratamento_ID { get; set; }

        public string Patologia { get; set; }

        public string Grau { get; set; }

        public DateTime DataInicio {get;set;}

        public DateTime DataFim { get; set; }

        public string DuracaoCiclo { get; set; }

        public string Regime { get; set; }

        public bool Decorrer { get; set; }

        public bool Concluido { get; set; }


    }
}