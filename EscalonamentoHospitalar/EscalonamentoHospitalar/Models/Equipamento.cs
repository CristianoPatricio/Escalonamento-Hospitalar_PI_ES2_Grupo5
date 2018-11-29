using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class Equipamento
    {
        public int EquipamentoId { get; set; }

        public string Nome { get; set; }

        public int Quantidade { get; set; }
    }
}
