using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class EspecialidadeEnfermeiro
    {
        public int EspecialidadeEnfermeiroId { get; set; }

        [Required(ErrorMessage = "Por favor, introduza um nome para a especialidade")]
        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Nome inválido")]
        public string Especialidade { get; set; }
    
        public ICollection<Enfermeiro> Enfermeiro { get; set; }
        public ICollection<EnfermeiroEspecialidade> EnfermeirosEspecialidade { get; set; }
    }
}
