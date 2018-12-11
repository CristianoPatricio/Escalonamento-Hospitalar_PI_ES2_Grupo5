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

        public Enfermeiro Enfermeiro { get; set; }
        public int EnfermeiroRequerenteId { get; set; }

        public HorarioEnfermeiro HorarioEnfermeiro { get; set; }
        public int HorarioATrocarId { get; set; }
        public int EnfermeiroATrocarId { get; set; }

        public int HorarioParaTrocaId { get; set; }     
        public int EnfermeiroParaTrocaId { get; set; }

        public EstadoPedidoTroca EstadoPedidoTroca { get; set; }
        public int EstadoPedidoTrocaId { get; set; }     
    }
}
