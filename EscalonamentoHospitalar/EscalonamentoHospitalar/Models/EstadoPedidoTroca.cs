using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class EstadoPedidoTroca
    {
        public int EstadoPedidoTrocaId { get; set; }

        public string Nome { get; set; }

        public ICollection<PedidoTrocaTurnosEnfermeiro> PedidoTrocaTurnosEnfermeiros { get; set; }
    }
}
