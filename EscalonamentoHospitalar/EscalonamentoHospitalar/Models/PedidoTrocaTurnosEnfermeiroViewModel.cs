using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class PedidoTrocaTurnosEnfermeiroViewModel
    {
        public HorarioEnfermeiro horarioEnfermeiroATrocar { get; set; }
        public HorarioEnfermeiro horarioEnfermeiroParaTroca { get; set; }
    }
}
