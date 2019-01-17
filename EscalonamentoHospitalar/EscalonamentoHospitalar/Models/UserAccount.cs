using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public class UserAccount
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Por favor introduza o número mecanográfico respetivo")]
        public string NumeroMecanografico { get; set; }

        [Required(ErrorMessage = "Por favor introduza o nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor introduza o número mecanográfico respetivo")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Por favor introduza a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As passwords não são iguais")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
