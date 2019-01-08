using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class HorarioParaTrocaMedico
    {
        public int HorarioParaTrocaMedicoId { get; set; }

        public HorarioMedico HorarioMedico { get; set; }
        public int HorarioMedicoId { get; set; }

        public ICollection<PedidoTrocaTurnosMedico> PedidoTrocaTurnosMedicos { get; set; }

    }
}
