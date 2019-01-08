using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class HorarioMedico
    {
        public int HorarioMedicoId { get; set; }
        public DateTime DataInicioTurno { get; set; }
        public int Duracao { get; set; }
        public DateTime DataFimTurno { get; set; }

        public int TurnoId { get; set; }
        public Turno Turno { get; set; }

        public int MedicoId { get; set; }
        public Medico Medico { get; set; }

        public ICollection<HorarioATrocarMedico> HorarioATrocarMedicos { get; set; }
        public ICollection<HorarioParaTrocaMedico> HorarioParaTrocaMedicos { get; set; }
    }
}
