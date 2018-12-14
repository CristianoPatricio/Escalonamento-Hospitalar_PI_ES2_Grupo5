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
        public int EnfermeiroId { get; set; }

        public HorarioATrocarEnfermeiro HorarioATrocarEnfermeiro { get; set; }
        public int HorarioATrocarEnfermeiroId { get; set; }

        public HorarioParaTrocaEnfermeiro HorarioParaTrocaEnfermeiro { get; set; }
        public int HorarioParaTrocaEnfermeiroId { get; set; }

        public EstadoPedidoTroca EstadoPedidoTroca { get; set; }
        public int EstadoPedidoTrocaId { get; set; }     
    }
}
