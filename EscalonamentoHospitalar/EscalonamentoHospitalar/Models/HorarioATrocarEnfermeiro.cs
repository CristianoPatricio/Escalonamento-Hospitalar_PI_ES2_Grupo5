using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class HorarioATrocarEnfermeiro
    {
        public int HorarioATrocarEnfermeiroId { get; set; }

        public HorarioEnfermeiro HorarioEnfermeiro { get; set; }
        public int HorarioATrocarId { get; set; }

        public ICollection<PedidoTrocaTurnosEnfermeiro> pedidoTrocaTurnosEnfermeiros { get; set; }
    }
}
