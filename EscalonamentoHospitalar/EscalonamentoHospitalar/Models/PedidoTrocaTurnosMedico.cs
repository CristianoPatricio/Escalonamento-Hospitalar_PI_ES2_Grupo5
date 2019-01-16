using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class PedidoTrocaTurnosMedico
    {
        public int PedidoTrocaTurnosMedicoId { get; set; }

        public DateTime DataPedido { get; set; }

        public Medico Medico { get; set; }
        public int MedicoId { get; set; }

        public HorarioATrocarMedico HorarioATrocarMedico { get; set; }
        public int HorarioATrocarMedicoId { get; set; }

        public HorarioParaTrocaMedico HorarioParaTrocaMedico { get; set; }
        public int HorarioParaTrocaMedicoId { get; set; }

        public EstadoPedidoTroca EstadoPedidoTroca { get; set; }
        public int EstadoPedidoTrocaId { get; set; }

    }
}
