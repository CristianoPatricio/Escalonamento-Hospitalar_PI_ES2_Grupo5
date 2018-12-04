﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class GerarEscala
    {
        [Required (ErrorMessage = "Por favor, introduza o número de pessoas para o turno 1")]
        public int NumeroPessoasTurno1 { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o número de pessoas para o turno 2")]
        public int NumeroPessoasTurno2 { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o número de pessoas para o turno 3")]
        public int NumeroPessoasTurno3 { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Por favor, indique a data de ínicio da semana")]
        [DisplayFormat (DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataInicioSemana { get; set; }
    }
}