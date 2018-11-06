using System.Collections.Generic;

namespace EscalonamentoHospitalar.Models
{
    public class Especialidade
    {
        public int EspecialidadeId { get; set; }
        public string Nome { get; set; }

       // public ICollection<Medicos> Medicos { get; set; }
        public ICollection<MedicoEspecialidade> MedicoEspecialidade { get; set; }
        public ICollection<EnfermeiroEspecialidade> EnfermeiroEspecialidade { get; set; }
    }
}