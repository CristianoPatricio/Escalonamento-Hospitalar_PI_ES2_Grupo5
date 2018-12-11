using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class HorarioEnfermeiro
    {
        public int HorarioEnfermeiroId { get; set; }
        public DateTime DataInicioTurno { get; set; }
        public int Duracao { get; set; }
        public DateTime DataFimTurno { get; set; }

        public int TurnoId { get; set; }
        public Turno Turno { get; set; }

        public int EnfermeiroId { get; set; }
        public Enfermeiro Enfermeiro { get; set; }

        public ICollection<PedidoTrocaTurnosEnfermeiro> pedidoTrocaTurnosEnfermeiros { get; set; }
    }
}
