using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class HorarioParaTrocaEnfermeiro
    {
        public int HorarioParaTrocaEnfermeiroId { get; set; }

        public HorarioEnfermeiro HorarioEnfermeiro { get; set; }
        public int HorarioParaTrocaId { get; set; }

        public ICollection<PedidoTrocaTurnosEnfermeiro> pedidoTrocaTurnosEnfermeiros { get; set; }

    }
}
