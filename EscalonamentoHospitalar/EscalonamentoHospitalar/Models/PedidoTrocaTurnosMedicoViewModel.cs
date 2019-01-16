using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class PedidoTrocaTurnosMedicoViewModel
    {
        public HorarioMedico horarioMedicoATrocar { get; set; }
        public HorarioMedico horarioMedicoParaTroca { get; set; }
    }
}
