using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Auth.DTO
{
    public class UserInfo
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Login { get; set; } = null!;
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Password { get; set; } = null!;  
        public void ClearInputFields()
        {
            Login = String.Empty;
            Password = String.Empty;
        }
    }
    public class Userrole
    {
        public string? Login { get; set; }

    }
}
