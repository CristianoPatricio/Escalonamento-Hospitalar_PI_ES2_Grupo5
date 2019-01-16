using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class HorarioPacienteViewModel
    {
        public IEnumerable<HorarioPaciente> HorariosPacientes { get; set; }
        public PagingViewModel Pagination { get; set; }

        public string CurrentNome { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data inválida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? DataInicio { get; set; }
    }
}
