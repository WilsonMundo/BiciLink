using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Auth.DTO
{
    public class UserRegister : UserInfo
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public string confirmPassword { get; set; } = null!;
        [Required(ErrorMessage = "Este campo es requerido")]
        public string name { get; set; } = null!;
        public string? direccion { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string nit { get; set; } = null!;
        [Required(ErrorMessage = "Este campo es requerido")]
        public string telefono { get; set; } = null!;
    }
}
