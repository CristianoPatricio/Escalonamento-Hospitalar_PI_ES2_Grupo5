using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class PedidoTrocaTurnosEnfermeiro
    {
        public int PedidoTrocaTurnosEnfermeiroId { get; set; }

        public DateTime DataPedido { get; set; }

        public Enfermeiro EnfermeiroRequerente { get; set; }
        public int EnfermeiroRequerenteId { get; set; }

        public HorarioATrocarEnfermeiro HorarioATrocarEnfermeiro { get; set; }
        public int HorarioATrocarId { get; set; }

        public HorarioParaTrocaEnfermeiro HorarioParaTrocaEnfermeiro { get; set; }
        public int HorarioParaTrocaId { get; set; }

        public EstadoPedidoTroca EstadoPedidoTroca { get; set; }
        public int EstadoPedidoTrocaId { get; set; }     
    }
}
